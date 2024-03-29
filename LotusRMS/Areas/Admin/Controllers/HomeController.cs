﻿using LotusRMS.Models;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Utility;
using LotusRMS.Utility.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LotusRMS.Utility.Enum.Enums;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly IOrderService _iOrderService;
        private readonly ICheckoutService _iCheckoutService;
        private readonly IMenuService _iMenuService;
        private readonly ICustomerService _iCustomerService;
        public HomeController(IOrderService iOrderService,
            ICheckoutService iCheckoutService,
            IMenuService iMenuService,
            ICustomerService iCustomerService)
        {
            _iOrderService = iOrderService;
            _iCheckoutService = iCheckoutService;
            _iMenuService = iMenuService;
            _iCustomerService = iCustomerService;
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
        public async Task<IActionResult> GetCustomerDue()
        {
            var customer =await _iCustomerService.GetAllAvailableAsync().ConfigureAwait(true);
            var count = 0;
            var due = 0.0;
            if (customer != null)
            {
                count = customer.Count();
                if (count > 0)
                {
                    foreach(var cus in customer)
                    {
                        if (cus.DueBooks.Count() > 0)
                        {
                            due += cus.DueBooks.LastOrDefault().BalanceDue;
                        }

                    }
                   
                }
            }
            return Json(new { count = count, due = Math.Round(due,2) });
        
        }
        public async Task<IActionResult> GetTableBooked(ReportType type)
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
            var tableBookedCount = await _iOrderService.GetAllByDateRangeAsync(startDate, endDate).ConfigureAwait(true);
            if (tableBookedCount != null)
            {

                return Json(new { data = tableBookedCount.Count() });
            }
            else
            {
                return Json(new { data = 0 });
            }
        }
       public async Task<IActionResult> GetStandingOrder()
        {
            var active = await _iOrderService.GetAllActiveOrderAsync().ConfigureAwait(true)?? throw new Exception();
            var data = GetItemDetail(active).Where(x=>!x.IsComplete).GroupBy(f=>new { f.MenuId ,f.Quantity_Id,f.Item_Name}).
                Select(group=> new { fee=group.Key,total=group.Sum(f=>f.Quantity)});
            return Json(new { data = data });
        }
        public async Task<IActionResult> GetTransection(ReportType type){
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
            var totalTransection =await _iCheckoutService.GetAllByDateRangeAsync(startDate, endDate) ?? throw new Exception();
            if (totalTransection != null)
            {
                return Json(new { data = totalTransection.Sum(x => x.Total) });
            }else{
                return Json(new { data = 0 });
            }
        }
        public async Task<IActionResult> GetTop5Item()
        {
            var todayStart = Convert.ToDateTime(CurrentTime.DateTimeToday());
            var yesterdayStart= todayStart.AddDays(-1);
            var todayEnd = (todayStart).AddDays(1).AddMilliseconds(-1);
            var yesterdayEnd = todayEnd.AddDays(-1);


            var yeaterdayOrder =await _iOrderService.GetAllByDateRangeAsync(yesterdayStart, yesterdayEnd).ConfigureAwait(true);
             var todayOrder = await _iOrderService.GetAllByDateRangeAsync(todayStart, todayEnd).ConfigureAwait(true);

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
            return new JsonResult(dataset);
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
                    foreach (var o in od)
                    {
                        var dd = o.Select(y => new OrderDetailVm()
                        {
                            MenuId = y.MenuId,
                            Quantity = y.Quantity,
                            Item_Name = y.Menu.Item_Name + "(" + y.Menu.Menu_Details.FirstOrDefault(x => x.Id == y.Quantity_Id).Divison.Title + ")",
                            Quantity_Id = y.Quantity_Id,
                            IsComplete = y.IsComplete

                        }).ToList();
                        ordervm.AddRange(dd);
                    } 
            }
            return ordervm;
        }

        #endregion

    }
}