using LotusRMS.Models;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Utility;
using LotusRMS.Utility.Enum;
using Microsoft.AspNetCore.Mvc;
using static LotusRMS.Utility.Enum.Enums;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IOrderService _iOrderService;
        private readonly ICheckoutService _iCheckoutService;

        public HomeController(IOrderService iOrderService, 
            ICheckoutService iCheckoutService)
        {
            _iOrderService = iOrderService;
            _iCheckoutService = iCheckoutService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UnitTest()
        {
            return View();
        }
        #region APICalls
        public IActionResult GetTableBooked(ReportType type)
        {
            var today = CurrentTime.DateTimeToday();
            var startDate = Convert.ToDateTime(today);
            var endDate = CurrentTime.DateTimeNow();
            if (type == ReportType.Week)
            {
                startDate = startDate.AddDays(-7);
            }else if (type == ReportType.Month)
            {
                startDate = startDate.AddDays(-startDate.Day+1);
            }else if (type == ReportType.Year)
            {
                startDate = startDate.AddMonths(-startDate.Month+1).AddDays(-startDate.Day+1);
            }
            var tableBookedCount = _iOrderService.GetAllByDateRange(startDate, endDate);

            return Json(new {data=tableBookedCount.Count()});
        }
       public IActionResult GetStandingOrder()
        {
            var active = _iOrderService.GetAllActiveOrder();
       /*     var data = GetItemDetail(active).AsEnumerable().Select(x=>x.MenuId).GroupBy(x => new { x.MenuId, x.Quantity_Id,x.Quantity,x.Item_Name}).Select(sd=> new
            {
                Item_Name=sd.,

            });*/
            return Json(new { data = "" });
        }
        public IActionResult GetTransection(ReportType type){
            var today = CurrentTime.DateTimeToday();
            var startDate = Convert.ToDateTime(today);
            var endDate = CurrentTime.DateTimeNow();
            if (type == ReportType.Week)
            {
                startDate = startDate.AddDays(-7);
            }
            else if (type == ReportType.Month)
            {
                startDate = startDate.AddDays(-startDate.Day + 1);
            }
            else if (type == ReportType.Year)
            {
                startDate = startDate.AddMonths(-startDate.Month + 1).AddDays(-startDate.Day + 1);
            }
            var totalTransection = _iCheckoutService.GetAllByDateRange(startDate, endDate).Sum(x=>x.Total);
            
            return Json(new { data = totalTransection });
        }
        public IActionResult GetTop5Item()
        {
            var todayStart = Convert.ToDateTime(CurrentTime.DateTimeToday());
            var yesterdayStart= todayStart.AddDays(-1);
            var todayEnd = (todayStart).AddDays(1).AddMilliseconds(-1);
            var yesterdayEnd = todayEnd.AddDays(-1);


            var yeaterdayOrder = _iOrderService.GetAllByDateRange(yesterdayStart, yesterdayEnd);
             var todayOrder = _iOrderService.GetAllByDateRange(todayStart, todayEnd);

            var yesterdayData = GetItemDetail(yeaterdayOrder).GroupBy(x => x.Item_Name).Select(grouped => new
            {
                key = grouped.Key,
                orderDetail = grouped.OrderBy(x => x.Quantity).Sum(x=>x.Quantity)

            });
            var todayData = GetItemDetail(todayOrder).GroupBy(x => x.Item_Name).Select(grouped => new
            {
                key = grouped.Key,
                orderDetail = grouped.OrderBy(x => x.Quantity).Sum(x=>x.Quantity)

            });
            var dataset = GetDataSet(yesterdayData,todayData);
            return Ok(dataset);
        }
        public List<Object> GetDataSet(IEnumerable<dynamic> yesterdayData, IEnumerable<dynamic> todayData)
        {

            var newyd = yesterdayData.OrderByDescending(x => x.orderDetail);
            var newTD = todayData.OrderByDescending(x => x.orderDetail);
            var nedata = yesterdayData.UnionBy(todayData,x=>x.key).ToList().OrderByDescending(y=>y.orderDetail);


            var dataset = new List<object>();

            foreach (var data in nedata.Select((item, index) => (item, index)))
            {
                var yesterdays = newyd.Where(x => x.key == data.item.key).FirstOrDefault();
                var todays = newTD.Where(x => x.key == data.item.key).FirstOrDefault();
                if (yesterdays == null && todays!=null)
                {
                    dataset.Add(new List<string>() { data.item.key, ""+todays.orderDetail+"", "0" });
                }else if(todays==null && yesterdays != null)
                {
                    dataset.Add(new List<string>() { data.item.key, "0", "" +yesterdays.orderDetail+""});
                }
                else
                {
                    dataset.Add(new List<string>() { data.item.key, ""+todays.orderDetail+"", "" + yesterdays.orderDetail + "" });
                }
                if (data.index == 4)
                {
                    break;
                }
               
            }
            if (dataset.Count() == 0)
            {
                dataset.Add(new List<string>() { "NaN", "5", "10" });
            }
            return dataset;
        }
        public List<OrderDetailVm> GetItemDetail(IEnumerable<LotusRMS_Order> order)
        {
            var ordervm= new List<OrderDetailVm>();

            if (order != null)
            {
                var od = order.Select(x => x.Order_Details);
                foreach(var o in od)
                {
                    var dd = o.Select(y => new OrderDetailVm()
                    {
                        MenuId=y.MenuId,
                        Quantity=y.Quantity,
                        Item_Name=y.Menu.Item_Name,
                        Quantity_Id=y.Quantity_Id

                    }).ToList();
                    ordervm.AddRange(dd);
                }


            }


            return ordervm;
        }

        #endregion

    }
}