using DocumentFormat.OpenXml.Drawing.Charts;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Checkout;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMSweb.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LotusRMSweb.Areas.Kitchen.Controllers
{
    [Area("Kitchen")]
    public class HomeController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        private readonly IHubContext<OrderHub, IOrderHub> _orderHub;

        public HomeController(ITableService tableService, 
            IOrderService orderService, 
            IMenuService menuService,
            IHubContext<OrderHub, IOrderHub> orderHub)
        {
            _tableService = tableService;
            _orderService = orderService;
            _menuService = menuService;
            _orderHub = orderHub;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region Api Call
        public async Task<IActionResult> GetData() {

            var tables =await _tableService.GetAllReservedAsync().ConfigureAwait(true);
            var orderVM=new List<PrintOrderDetailVM>();   
            foreach(var table in tables) {
                var order = await _orderService.GetFirstOrDefaultByTableIdAsync(table.Id).ConfigureAwait(true);
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
                        var menu = await _menuService.GetFirstOrDefaultByIdAsync(item.MenuId).ConfigureAwait(true);
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
                                Remarks=item.Remarks
                                
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


        public async Task<IActionResult> CompleteKitchen(string orderNo,Guid orderDetailId)
        {
            var orderId = await _orderService.UpdateKitchenCompleteAsync(orderNo, orderDetailId).ConfigureAwait(true);
            var order = await _orderService.GetFirstOrDefaultByOrderNoAsync(orderNo).ConfigureAwait(true);
            
            var orderDetail = order.Order_Details.Where(x => x.Id == orderDetailId).FirstOrDefault();
            var menu = await _menuService.GetByGuidAsync(orderDetail.MenuId).ConfigureAwait(true);
            var data = new List<string>()
            {
                order.Table.Table_Name,
                menu.Item_Name
            };
            await _orderHub.Clients.All.OrderComplete(data);
            return Ok(orderId);
        }

        #endregion 
    }
}
