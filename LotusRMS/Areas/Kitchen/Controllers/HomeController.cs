using DocumentFormat.OpenXml.Drawing.Charts;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Checkout;
using LotusRMS.Models.Viewmodels.Order;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Kitchen.Controllers
{
    [Area("Kitchen")]
    public class HomeController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public HomeController(ITableService tableService, 
            IOrderService orderService, 
            IMenuService menuService)
        {
            _tableService = tableService;
            _orderService = orderService;
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region Api Call
        public IActionResult GetData() {

            var tables = _tableService.GetAllReserved();
            var orderVM=new List<PrintOrderDetailVM>();   
            foreach(var table in tables) {
                var order = _orderService.GetFirstOrDefaultByTableId(table.Id);
                if (order.Order_Details.Any(x => !x.IsKitchenComplete && !x.IsComplete))
                {
                    var kitchenOrderVM = new PrintOrderDetailVM()
                {
                    OrderNo = order.Order_No,
                    TableName = order.Table.Table_Name,
                    OrderDetail = new List<OrderDetailVm>()
                };

                   

                    foreach (var item in order.Order_Details.Where(x => !x.IsKitchenComplete && !x.IsComplete ))
                    {
                        var menu = _menuService.GetFirstOrDefault(item.MenuId);
                        if (menu.OrderTo == "Kitchen")
                        {
                            var orderDetail = new OrderDetailVm()
                            {
                                Id = item.Id,
                                MenuId = item.MenuId,
                                Item_Name = menu.Item_Name,
                                Item_Unit = menu.Menu_Unit.Unit_Symbol,
                                Rate = item.Rate,
                                Quantity = item.Quantity,
                                IsComplete = item.IsComplete,
                                IsPrinted = item.IsPrinted,
                                IsKitchenComplete = item.IsKitchenComplete,
                                Remarks="i have order here",
                                Total = item.GetTotal
                            };
                            kitchenOrderVM.OrderDetail.Add(orderDetail);
                        }

                    }
                    orderVM.Add(kitchenOrderVM);
                }
               
            }

            return PartialView("_OrderView", orderVM);
                /*Json(new {data=orderVM});*/

}
        #endregion 
    }
}
