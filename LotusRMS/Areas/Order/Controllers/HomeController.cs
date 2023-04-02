using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.OrderDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMSweb.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Differencing;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LotusRMSweb.Areas.Order.Controllers
{
    [Area("Order")]
    [Authorize(Roles ="Admin,SuperAdmin,Waiter,Cashier,Bar")]
    public class HomeController : Controller
    {

        private readonly ITableTypeService _ITableTypeService;
        private readonly ITableService _ITableService;
        private readonly IMenuService _IMenuService;
        private readonly UserManager<RMSUser> _UserManager;
        private readonly IHubContext<OrderHub,IOrderHub> _orderHub;
        private readonly IOrderService _IOrderService;
        public HomeController(IOrderService iOrderService,ITableTypeService TableTypeService, ITableService iTableService, IMenuService iMenuService,
            UserManager<RMSUser> userManager,
            IHubContext<OrderHub, IOrderHub> orderHub
            )
        {
            _IOrderService = iOrderService;
            _ITableTypeService = TableTypeService;
            _ITableService = iTableService;
            _IMenuService = iMenuService;
            _UserManager = userManager;
            _orderHub = orderHub;
        }


        public IActionResult Index()
        {
            var type = _ITableTypeService.GetAll().Where(x => !x.IsDelete && x.Status);
            var menu = _IMenuService.GetAll().Where(x => !x.IsDelete && x.Status).Select(menu => new OrderMenu()
            {
                Item_Name = menu.Item_Name,
                Rate=menu.Rate,
                Symbol=menu.Menu_Unit.Unit_Symbol,
                Id = menu.Id
            }).ToList();
            ViewBag.Menu = menu;

            return View(type);
        }
        public IActionResult GetTable(Guid Id)
        {
            var table= _ITableService.GetAll().Where(x => !x.IsDelete && x.Status && x.Table_Type_Id==Id);
            return PartialView("_TableList", model: table);
        }

        public IActionResult GetOrder(Guid Id)
        {
            var order = GetOrderVM(Id,"");

            ViewBag.NewOrder = GetNewOrder(Id);


            return PartialView("_Order",model:order);
        }

        public IActionResult Selectmenu(Guid MenuId,Guid TableId)
        {
            var menu=_IMenuService.GetByGuid(MenuId);
            var vm = new AddNewOrderVM()
            {
                MenuId = MenuId,
                TableId=TableId,
                Item_Name = menu.Item_Name,
                Rate = menu.Rate,
                Quantity = 0

            };
            
            return PartialView("_AddMenu",model:vm);
        }
        public OrderVm GetOrderVM(Guid tableId,string? orderNo)
        {
            var OrderVM = new OrderVm()
            {
            Order_Details = new List<OrderDetailVm>()
            };
            var order = new LotusRMS_Order();
            if (tableId != Guid.Empty)
            {
                OrderVM.TableId = tableId;
                OrderVM.Table_Name = _ITableService.GetByGuid(tableId).Table_Name;
                order = _IOrderService.GetFirstOrDefaultByTableId(tableId);
                if (order != null)
                {
                    OrderVM = new OrderVm()
                    {
                        Id = order.Id,
                        TableId = order.Table_Id,
                        Table_Name=order.Table.Table_Name,
                        Date = order.DateTime,
                        Order_No = order.Order_No,
                        Order_Details = new List<OrderDetailVm>()
                    };
                    foreach(var item in order.Order_Details)
                    {
                        var menu = _IMenuService.GetFirstOrDefault(item.MenuId);
                        var orderDetail = new OrderDetailVm()
                        {
                            Id = item.Id,
                            MenuId = item.MenuId,
                            Item_Name = menu.Item_Name,
                            Item_Unit=menu.Menu_Unit.Unit_Symbol,
                            Rate = item.Rate,
                            Remarks=item.Remarks,
                            Quantity = item.Quantity,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete,
                            Total = item.GetTotal
                        };
                        OrderVM.Order_Details.Add(orderDetail);
                    }
                }
            } else if (orderNo != null)
            {
                order = _IOrderService.GetFirstOrDefaultByOrderNo(orderNo);
                if (order != null)
                {
                    OrderVM = new OrderVm()
                    {
                        Id = order.Id,
                        TableId = order.Table_Id,

                        Table_Name = order.Table.Table_Name,
                        Date = order.DateTime,
                        Order_No = order.Order_No,
                        Order_Details = new List<OrderDetailVm>()
                    };
                    foreach (var item in order.Order_Details)
                    {

                        var menu = _IMenuService.GetFirstOrDefault(item.MenuId);
                        var orderDetail = new OrderDetailVm()
                        {
                            Id = item.Id,
                            MenuId = item.MenuId,
                            Item_Name = menu.Item_Name,
                            Item_Unit= menu.Menu_Unit.Unit_Symbol ,
                            Rate = item.Rate,
                            Remarks = item.Remarks,
                            Quantity = item.Quantity,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete,
                            Total = item.GetTotal
                        };
                        OrderVM.Order_Details.Add(orderDetail);
                    }
                }
                OrderVM.TableId = order.Table_Id;
                OrderVM.Table_Name = order.Table.Table_Name;
            }
            return OrderVM;
        }

        //new order region
        #region newOrder
        [HttpPost]
        public IActionResult AddNewOrder(AddNewOrderVM vm)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(vm.TableId.ToString()) != null )
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(vm.TableId.ToString()));
            }
            var menu=_IMenuService.GetAll().Where(x=>x.Id== vm.MenuId).FirstOrDefault();
            vm.Item_Name = menu.Item_Name;
            vm.Item_Unit= menu.Menu_Unit.Unit_Symbol;
            orderList.Add(vm);

            HttpContext.Session.SetString(vm.TableId.ToString(), JsonConvert.SerializeObject(orderList));


            return PartialView("_NewOrders",model:orderList);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteNewOrder(Guid tableId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(tableId.ToString()) != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(tableId.ToString()));
            }

            var orderDetailDTO = new List<CreateOrderDetailDTO>();
            foreach (var item in orderList)
            {
                var detailDto = new CreateOrderDetailDTO()
                {
                    Menu_Id = item.MenuId,
                    Quantity = item.Quantity,
                    Rate = item.Rate,
                    Remarks=item.Remarks
                };
                orderDetailDTO.Add(detailDto);
            }
            var order = _IOrderService.GetFirstOrDefaultByTableId((Guid)tableId);
            if (order != null)
            {
                var dto = new UpdateOrderDTO()
                {
                    Order_Id = order.Id,
                    OrderDetail = orderDetailDTO
                };
                _IOrderService.UpdateCompleteOrder(dto);

            }
            else {

                var dto = new CreateOrderDTO()
                {
                    Table_Id = tableId,
                    UserId = userId,
                    OrderDetails = orderDetailDTO
                };
                var id = _IOrderService.Create(dto);
                if(id!=Guid.Empty){
                    _ITableService.UpdateReserved(tableId);
                }
            }


            HttpContext.Session.Remove(tableId.ToString());
            await _orderHub.Clients.All.OrderReceived(tableId);
            var orders = GetOrderVM(tableId, "");
            
            ViewBag.NewOrder = GetNewOrder(tableId);
            return PartialView("_Order",model:orders);
        }
        public IEnumerable<AddNewOrderVM> GetNewOrder(Guid tableId)
        {
            var newOrder = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(tableId.ToString()) != null)
            {
                newOrder = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(tableId.ToString()));
            }
            return newOrder;
        }
        [HttpGet]
        public IActionResult DeleteNewOrder(Guid tableId,Guid menuId,float quantity)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(tableId.ToString()) != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(tableId.ToString()));
            }

            var order = orderList.Where(x => x.MenuId == menuId && x.Quantity == quantity).FirstOrDefault();
            orderList.Remove(order);

            HttpContext.Session.SetString(tableId.ToString(), JsonConvert.SerializeObject(orderList));

            return PartialView("_NewOrders", orderList);
        }
        [HttpPost]
        public IActionResult EditNewOrder(Guid tableId,Guid menuId,float quantity,string remarks)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(tableId.ToString()) != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(tableId.ToString()));
            }

            var order = orderList.Where(x => x.MenuId == menuId && x.Quantity == quantity).FirstOrDefault();
            orderList.Remove(order);

            HttpContext.Session.SetString(tableId.ToString(), JsonConvert.SerializeObject(orderList));

            var menu = _IMenuService.GetByGuid(menuId);

            var vm = new AddNewOrderVM()
            {
                MenuId = menuId,
                TableId = tableId,
                Item_Name = menu.Item_Name,
                Rate = menu.Rate,
                Quantity = quantity,
                Remarks= remarks


            };

            return PartialView("_AddMenu", model: vm);
        }
        #endregion

        //new order region
        [HttpPost]
        public IActionResult CancelOrder(string orderNo, Guid OrderDetailId)
        {

            var id = _IOrderService.CancelOrder(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);
            ViewBag.NewOrder = GetNewOrder(order.TableId);
            return PartialView("_Order", model: order);
        }

        [HttpPost]
        public IActionResult CompleteOrderDetail(string orderNo,Guid OrderDetailId)
        {

            var id = _IOrderService.CompleteOrderDetail(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);
            ViewBag.NewOrder = GetNewOrder(order.TableId);
            return PartialView("_Order", model: order);
        }
        [HttpGet]
        public IActionResult ReturnTableType(Guid Id)
        {
            var table = _ITableService.GetFirstOrDefaultById(Id);
            var bookedCount = _ITableService.GetAllByTypeId(table.Table_Type_Id).Where(x => x.IsReserved).Count();
            return Json(new { typeId = table.Table_Type_Id, count = bookedCount });
        }

    }
}
