using DocumentFormat.OpenXml.Drawing.Charts;
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

        public async Task<IActionResult> Index(Guid? TableId,Guid? TypeId)
        {
            
            var type = await _ITableTypeService.GetAllAvailableAsync();
            var tableType = new List<TableTypeBookedVM>();
            if (type != null)
            {
                foreach (var item in type)
                {
                    var tablesCount = (await _ITableService.GetAllByTypeIdAsync(item.Id)).Where(x => x.IsReserved).Count();
                    var tvm = new TableTypeBookedVM()
                    {
                        Type_Id = item.Id,
                        Type_Name = item.Type_Name,
                        BookedCount = tablesCount
                    };
                    tableType.Add(tvm);
                }
            }
            var menu =(await _IMenuService.GetAllAvailableAsync().ConfigureAwait(true)).Select(nmenu => new OrderMenu()
            {
                Item_Name = nmenu.Item_Name,
                Symbol= nmenu.Menu_Details.FirstOrDefault(x => x.Default).Divison.Title, //+ "( "+menu.Menu_Details.FirstOrDefault(x => x.Default).Divison.Value+" " + menu.Menu_Unit.Unit_Symbol+")",
                Rate=nmenu.Menu_Details.FirstOrDefault(x=>x.Default).Rate,
                Id = nmenu.Id
            }).ToList();
            ViewBag.Menu = menu;
            ViewBag.TableId = TableId;
            ViewBag.TypeId = TypeId;
            return View(tableType);
        }
        public async Task<IActionResult> GetTable(Guid Id)
        {
            var table=(await _ITableService.GetAllAvailableAsync().ConfigureAwait(true)).Where(x => x.Table_Type_Id==Id);
            return PartialView("_TableList", model: table);
        }

        public async Task<IActionResult> GetOrder(Guid Id)
        {
            var order =await GetOrderVM(Id,"");
            ViewBag.NewOrder =await GetNewOrder(Id);
            return PartialView("_Order",model:order);
        }

        public async Task<IActionResult> Selectmenu(Guid MenuId,Guid TableId)
        {
            var menu=await _IMenuService.GetFirstOrDefaultByIdAsync(MenuId).ConfigureAwait(true);
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
        public async Task<OrderVm> GetOrderVM(Guid tableId,string? orderNo)
        {
            var OrderVM = new OrderVm()
            {
            Order_Details = new List<OrderDetailVm>()
            };
            var order = new LotusRMS_Order();
            if (tableId != Guid.Empty)
            {
                OrderVM.TableId = tableId;
                OrderVM.Table_Name = (await _ITableService.GetByGuidAsync(tableId)).Table_Name;
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
                        var menu = await _IMenuService.GetFirstOrDefaultByIdAsync(item.MenuId).ConfigureAwait(true);
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

                        var menu = await _IMenuService.GetFirstOrDefaultByIdAsync(item.MenuId).ConfigureAwait(true);
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
        public async Task<IActionResult> AddNewOrder(AddNewOrderVM vm)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(vm.TableId.ToString()) != null )
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(vm.TableId.ToString()));
            }
            var menu=await _IMenuService.GetFirstOrDefaultByIdAsync(vm.MenuId).ConfigureAwait(true);
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
                    await _ITableService.UpdateReservedAsync(tableId);
                }
            }


            HttpContext.Session.Remove(tableId.ToString());
           
            var orders =await GetOrderVM(tableId, "");
            await SetNotification(tableId);


            ViewBag.NewOrder = GetNewOrder(tableId);
            return PartialView("_Order",model:orders);
        }
        public async Task<IEnumerable<AddNewOrderVM>> GetNewOrder(Guid tableId)
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
        public async Task<IActionResult> EditNewOrder(Guid tableId,Guid menuId,float quantity,string remarks)
        {
            var orderList = new List<AddNewOrderVM>();
            if (HttpContext.Session.GetString(tableId.ToString()) != null)
            {
                orderList = JsonConvert.DeserializeObject<List<AddNewOrderVM>>(HttpContext.Session.GetString(tableId.ToString()));
            }

            var order = orderList.Where(x => x.MenuId == menuId && x.Quantity == quantity).FirstOrDefault();
            orderList.Remove(order);

            HttpContext.Session.SetString(tableId.ToString(), JsonConvert.SerializeObject(orderList));

            var menu = await _IMenuService.GetFirstOrDefaultByIdAsync(menuId).ConfigureAwait(true);

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
            var order =await GetOrderVM(new Guid(), orderNo);
            ViewBag.NewOrder = GetNewOrder(order.TableId);
            await SetNotification(order.TableId);
            return PartialView("_Order", model: order);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrderDetail(string orderNo,Guid OrderDetailId)
        {

            var id = _IOrderService.CompleteOrderDetail(orderNo, OrderDetailId);
            var order =await GetOrderVM(new Guid(), orderNo);

            await SetNotification(order.TableId);
            ViewBag.NewOrder = GetNewOrder(order.TableId);
            return PartialView("_Order", model: order);
        }
        [HttpGet]
        public async Task<IActionResult> ReturnTableType(Guid Id)
        {
            var table = await _ITableService.GetFirstOrDefaultByIdAsync(Id);
            var bookedCount =(await _ITableService.GetAllByTypeIdAsync(table.Table_Type_Id)).Where(x => x.IsReserved).Count();
            return Json(new { typeId = table.Table_Type_Id, count = bookedCount });
        }

        public async Task SetNotification(Guid Table_Id) 
        {
            var typeId = (await _ITableService.GetFirstOrDefaultByIdAsync(Table_Id)).Table_Type_Id;
            var tableBooked = (await _ITableService.GetAllByTypeIdAsync(typeId)).Count(x => x.IsReserved);
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
            var IsReserved=await _ITableService.UpdateReservedAsync(tableId);
            var order =await GetOrderVM(tableId, "");
            ViewBag.NewOrder = GetNewOrder(tableId);
            await SetReleaseNotification(tableId);
            return PartialView("_Order", model: order);

        }
        public async Task SetReleaseNotification(Guid Table_Id)
        {
            var typeId =(await _ITableService.GetFirstOrDefaultByIdAsync(Table_Id)).Table_Type_Id;
            var tableBooked =(await _ITableService.GetAllByTypeIdAsync(typeId)).Count(x => x.IsReserved);
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
