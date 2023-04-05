﻿using LotusRMS.Models.Service;
using LotusRMS.Models;
using LotusRMS.Models.Viewmodels.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LotusRMS.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using LotusRMS.Models.Viewmodels.Checkout;
using DocumentFormat.OpenXml.Office2010.Excel;
using LotusRMS.Models.Dto.CheckoutDTO;
using LotusRMS.Utility;
using LotusRMS.Models.Viewmodels.FiscalYear;
using DocumentFormat.OpenXml.Drawing.Charts;
using LotusRMS.Models.Viewmodels.signalRVM;
using LotusRMSweb.Hubs;
using Microsoft.AspNetCore.SignalR;
using LotusRMS.Models.Viewmodels.Table;
using LotusRMS.Models.Viewmodels.Type;

namespace LotusRMSweb.Areas.Checkout.Controllers
{

    [Area("Checkout")]
    [Authorize(Roles = "Admin,SuperAdmin,Cashier")]
    public class HomeController : Controller
    {
        private readonly ITableTypeService _ITableTypeService;
        private readonly ITableService _ITableService;
        private readonly IMenuService _IMenuService;
        private readonly UserManager<RMSUser> _UserManager;
        private readonly IOrderService _IOrderService;
        private readonly ICheckoutService _ICheckoutService;
        private readonly IBillSettingService _IBillSettingService;
        private readonly ICustomerService _ICustomerService;

        private readonly IHubContext<OrderHub, IOrderHub> _orderHub;
        public HomeController(IOrderService iOrderService, 
            ITableTypeService TableTypeService,
            ITableService iTableService,
            IMenuService iMenuService, 
            ICheckoutService iCheckoutService, 
            UserManager<RMSUser> userManager,
            IBillSettingService iBillSettingService, 
            ICustomerService iCustomerService, 
            IHubContext<OrderHub, IOrderHub> orderHub)
        {
            _IOrderService = iOrderService;
            _ITableTypeService = TableTypeService;
            _ITableService = iTableService;
            _IMenuService = iMenuService;
            _UserManager = userManager;
            _ICheckoutService = iCheckoutService;
            _IBillSettingService = iBillSettingService;
            _ICustomerService = iCustomerService;
            _orderHub = orderHub;
        }
        public IActionResult Index()
        {
            if (_IBillSettingService.GetActive() == null) {
                
            }

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
            return View(tableType);
        }
        public IActionResult GetTable(Guid Id)
        {
            var table = _ITableService.GetAll().Where(x => !x.IsDelete && x.Status && x.Table_Type_Id == Id);
            return PartialView("_TableList", model: table);
        }
        public IActionResult GetOrder(Guid Id)
        {
            var order = GetOrderVM(Id, "");
            ViewBag.Checkout = CreateCheckOut(order.Id, order.Order_Details.Sum(x => x.Total));
            return PartialView("_Order", model: order);
        }


        public async Task<IActionResult> CompleteCheckout(CreateCheckoutVM vm)
        {
            var dto = new CreateCheckoutDTO()
            {
                Order_Id = vm.Order_Id,
                Customer_Id = vm.Customer_Id,
                Customer_Name = vm.Customer_Name,
                Customer_Address = vm.Customer_Address,
                Customer_Contact = vm.Customer_Contact,
                Total = vm.Total,
                Discount_Type = vm.Discount_Type,
                Discount = vm.Discount,
                Payment_Mode = vm.Payment_Mode,
                Paid_Amount=vm.Paid_Amount

            };
            var id=_ICheckoutService.Create(dto);
            var order = _IOrderService.GetByGuid(vm.Order_Id);
            _ITableService.UpdateReserved(order.Table_Id);


            await SetCheckoutNotification(order.Table_Id);
            return RedirectToAction("InvoicePrint", "Invoice", new {area="",Id = id.Result,returnUrl="/checkout" });

        }

        public OrderVm GetOrderVM(Guid tableId, string? orderNo)
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
                            Item_Unit = menu.Menu_Unit.Unit_Symbol,
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
            }
            else if (orderNo != null)
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
                            Item_Unit = menu.Menu_Unit.Unit_Symbol,
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

        public CreateCheckoutVM CreateCheckOut(Guid Order_Id,float Total)
        {
            var checkoutVm = new CreateCheckoutVM()
            {
                Order_Id=Order_Id,
                Total=Total
            };
            return checkoutVm;
        }
        #region API 
        [HttpGet]
        public IActionResult GetCustomerView()
        {
            return PartialView("_CustomerView");
        }
        [HttpGet]    
        public IActionResult GetAllCustomer()
        {
            var customer = _ICustomerService.GetAllAvailable().Select(x=>new CustomerCheckOutVM()
            {
                Id=x.Id,
                Name=x.Name,
                Address=x.Address,
                Contact=x.Contact,
                PanOrVat=x.PanOrVat,
                Due=GetDue(x.DueBooks)
                
            });

            return Json(new { data = customer });
        }
        public float GetDue(List<LotusRMS_DueBook> dueBook)
        {
            if (dueBook.Count==0)
            {
                return 0;
            }
            else
            {
                return dueBook.LastOrDefault().BalanceDue;
            }
        }

        public IActionResult PrintKOT(string OrderNo)
        {
            var order = _IOrderService.GetFirstOrDefaultByOrderNo(OrderNo);


            var printOrderVM = new PrintOrderDetailVM()
            {
                OrderNo=OrderNo,
                TableName=order.Table.Table_Name,
                OrderDetail=new List<OrderDetailVm>()
            };
            if (order.Order_Details.Any(x =>!x.IsPrinted))
            {

                foreach (var item in order.Order_Details.Where(x => !x.IsPrinted))
                {
                    var menu = _IMenuService.GetFirstOrDefault(item.MenuId);
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
                        Total = item.GetTotal
                    };
                    printOrderVM.OrderDetail.Add(orderDetail);

                }
            }
            else
            {
                foreach (var item in order.Order_Details)
                {
                    var menu = _IMenuService.GetFirstOrDefault(item.MenuId);
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
                        Total = item.GetTotal
                    };
                    printOrderVM.OrderDetail.Add(orderDetail);

                }
            }
            return View(printOrderVM);
        }
        public IActionResult PrintKOTComplete(string OrderNo)
        {
            var order = _IOrderService.GetFirstOrDefaultByOrderNo(OrderNo);
            var printOrderVM = new PrintOrderDetailVM()
            {
                OrderNo = OrderNo,
                TableName = order.Table.Table_Name,
                OrderDetail = new List<OrderDetailVm>()
            };
            List<LotusRMS_Order_Details> orderDetails=new List<LotusRMS_Order_Details>();
            foreach (var item in order.Order_Details.Where(x => !x.IsPrinted))
            {

                orderDetails.Add(item);

            }
            var id=_IOrderService.PrintKotAsync(order.Id, orderDetails);

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CompleteOrderDetail(string orderNo, Guid OrderDetailId)
        {
            var id = _IOrderService.CompleteOrderDetail(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);
           await SetOrderNotification(order.TableId);

            ViewBag.Checkout = CreateCheckOut(order.Id, order.Order_Details.Sum(x => x.Total));
            return PartialView("_Order", model: order);
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder(string orderNo, Guid OrderDetailId)
        {

            var id = _IOrderService.CancelOrder(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);

            await SetOrderNotification(order.TableId);

            ViewBag.Checkout = CreateCheckOut(order.Id, order.Order_Details.Sum(x => x.Total));
            return PartialView("_Order", model: order);
        }

        public IActionResult GetSwitchTableView(Guid TableId)
        {
            var typeTable = new List<TypeTableVM>();
            var type = _ITableTypeService.GetAllAvailable().Select(t=>new TypeVM()
            {
                Id=t.Id,
                Type_Name=t.Type_Name
            });
            foreach(var item in type)
            {
                var tables = _ITableService.GetAllByTypeId(item.Id).Select(tab => new TableVM() {
                    Id = tab.Id,
                    Table_Name=tab.Table_Name,
                    IsReserved=tab.IsReserved
                }).ToList();

                typeTable.Add(new TypeTableVM()
                {
                    Selected=TableId,
                    Type = item,
                    Table = tables
                });

            }
            return PartialView("_SwitchTableView", model: typeTable);

        }

        public IActionResult SwitchTable(Guid OldTable,Guid NewTable)
        {
            var order = _IOrderService.GetFirstOrDefaultByTableId(OldTable);
            order.Table_Id = NewTable;

            var status = _ITableService.UpdateReserved(OldTable);
            var newStatus = _ITableService.UpdateReserved(NewTable);

            return RedirectToAction(nameof(Index));
        }
        #endregion



        public async Task SetCheckoutNotification(Guid Table_Id)
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
        public async Task SetOrderNotification(Guid Table_Id)
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

    }
}
