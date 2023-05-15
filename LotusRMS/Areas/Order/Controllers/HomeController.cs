﻿using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.OrderDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Models.Viewmodels.signalRVM;
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

        public IActionResult Index(Guid? TableId,Guid? TypeId)
        {
            
            var type = _ITableTypeService.GetAll().Where(x => !x.IsDelete && x.Status).ToList();
            var tableType = new List<TableTypeBookedVM>();
            if (type != null)
            {
                foreach (var item in type)
                {
                    var tablesCount = _ITableService.GetAllByTypeId(item.Id).Where(x => x.IsReserved).Count();
                    var tvm = new TableTypeBookedVM()
                    {
                        Type_Id = item.Id,
                        Type_Name = item.Type_Name,
                        BookedCount = tablesCount
                    };
                    tableType.Add(tvm);
                }
            }
            var menu = _IMenuService.GetAll().Where(x => !x.IsDelete && x.Status).Select(menu => new OrderMenu()
            {
                Item_Name = menu.Item_Name,
                Symbol= menu.Menu_Details.FirstOrDefault(x => x.Default).Divison.Title, //+ "( "+menu.Menu_Details.FirstOrDefault(x => x.Default).Divison.Value+" " + menu.Menu_Unit.Unit_Symbol+")",
                Rate=menu.Menu_Details.FirstOrDefault(x=>x.Default).Rate,
                Id = menu.Id
            }).ToList();
            ViewBag.Menu = menu;
            ViewBag.TableId = TableId;
            ViewBag.TypeId = TypeId;
            return View(tableType);
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
            var menu=_IMenuService.GetFirstOrDefault(MenuId);
            var vm = new AddNewOrderVM()
            {
                Menu=menu,
                MenuId = MenuId,
                TableId=TableId,
                Item_Name = menu.Item_Name,
                Item_Unit= menu.Menu_Unit.Unit_Symbol,
                Rate=menu.Menu_Details.FirstOrDefault(x=>x.Default).Rate,
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
                        Date = order.DateTime.ToString(),
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
                            Item_Name = menu.Item_Name + "(" + menu.Menu_Details.FirstOrDefault(x => x.Id == item.Quantity_Id).Divison.Title + ")",
                            Item_Unit=menu.Menu_Unit.Unit_Symbol,
                            Rate = item.Rate,
                            Remarks=item.Remarks,
                            Quantity = item.Quantity,
                            Quantity_Id=item.Quantity_Id,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete
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
                        Date = order.DateTime.ToString(),
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
                            Quantity_Id = item.Quantity_Id,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete,
                            
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
            vm.Item_Name = menu.Item_Name +"("+menu.Menu_Details.FirstOrDefault(x=>x.Id==vm.Quantity_Id).Divison.Title+")";
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
                    Quantity_Id=item.Quantity_Id,
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
           
            var orders = GetOrderVM(tableId, "");
            await SetNotification(tableId);


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

            var menu = _IMenuService.GetFirstOrDefault(menuId);

            var vm = new AddNewOrderVM()
            {
                MenuId = menuId,
                Menu= menu,
                TableId = tableId,
                Item_Name = menu.Item_Name,
                Item_Unit = menu.Menu_Unit.Unit_Symbol,
                Rate = order.Rate,
                Quantity = order.Quantity,
                Quantity_Id=order.Quantity_Id,



            };

            return PartialView("_AddMenu", model: vm);
        }
        #endregion

        //new order region
        [HttpPost]
        public async Task<IActionResult> CancelOrder(string orderNo, Guid OrderDetailId)
        {

            var id = _IOrderService.CancelOrder(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);
            ViewBag.NewOrder = GetNewOrder(order.TableId);
            await SetNotification(order.TableId);
            return PartialView("_Order", model: order);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrderDetail(string orderNo,Guid OrderDetailId)
        {

            var id = _IOrderService.CompleteOrderDetail(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);

            await SetNotification(order.TableId);
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

        public async Task SetNotification(Guid Table_Id) 
        {
            var typeId = _ITableService.GetFirstOrDefaultById(Table_Id).Table_Type_Id;
            var tableBooked = _ITableService.GetAllByTypeId(typeId).Count(x => x.IsReserved);
            var tvm = new tableReturnVM()
            {
                Type_Id = typeId,
                Table_Id = Table_Id,
                BookCount = tableBooked


            };
            await _orderHub.Clients.All.OrderReceived(tvm);
        }
        public async Task<IActionResult> ReleaseTable(string OrderNo)
        {
            
            var tableId =_IOrderService.ReleaseTable(OrderNo);
            var IsReserved=_ITableService.UpdateReserved(tableId);
            var order = GetOrderVM(tableId, "");
            ViewBag.NewOrder = GetNewOrder(tableId);
            await SetReleaseNotification(tableId);
            return PartialView("_Order", model: order);

        }
        public async Task SetReleaseNotification(Guid Table_Id)
        {
            var typeId = _ITableService.GetFirstOrDefaultById(Table_Id).Table_Type_Id;
            var tableBooked = _ITableService.GetAllByTypeId(typeId).Count(x => x.IsReserved);
            var tvm = new tableReturnVM()
            {
                Type_Id = typeId,
                Table_Id = Table_Id,
                BookCount = tableBooked
            };
            await _orderHub.Clients.All.CheckoutComplete(tvm);
        }

    }
}
