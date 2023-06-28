using LotusRMS.Models.Service;
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
using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models.Viewmodels.Galla;
using System.Security.Claims;
using LotusRMS.Models.Viewmodels.Invoice;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Dto.InventoryDTO;

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
        private readonly IFiscalYearService _IFiscalYearService;
        private readonly ICustomerService _ICustomerService;
        private readonly INotyfService _notyf;
        private readonly IGallaService _gallaService;
        private readonly IInventoryService _iInventoryService;

        private readonly IHubContext<OrderHub, IOrderHub> _orderHub;
        public HomeController(IOrderService iOrderService,
            ITableTypeService TableTypeService,
            ITableService iTableService,
            IMenuService iMenuService,
            ICheckoutService iCheckoutService,
            UserManager<RMSUser> userManager,
            IBillSettingService iBillSettingService,
            ICustomerService iCustomerService,
            IHubContext<OrderHub, IOrderHub> orderHub
,
            INotyfService notyf,
            IGallaService gallaService,
            IFiscalYearService iFiscalYearService,
            IInventoryService iInventoryService)
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
            _notyf = notyf;
            _gallaService = gallaService;
            _IFiscalYearService = iFiscalYearService;
            _iInventoryService = iInventoryService;
        }
        public async Task<IActionResult> Index(Guid? TypeId,Guid? TableId)
        {
            var galla = await _gallaService.GetTodayGallaAsync().ConfigureAwait(true);
            var createGallaVM = new CreateGallaVM()
            {
                User = User.FindFirstValue("firstname") + " " + User.FindFirstValue("middlename") + " " + User.FindFirstValue("lastname"),
                Opening_Balance = 0
            };

            if (galla == null)
            {
                var date = Convert.ToDateTime(CurrentTime.DateTimeToday()).AddDays(-1).ToString();
                var userId= User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var lastGalla = await _gallaService.GetLastGallaAsync(userId).ConfigureAwait(true);
                if (lastGalla != null)
                {
                    createGallaVM.Opening_Balance = lastGalla.Closing_Balance;
                }
                ViewBag.Galla = 0;
            }
            else
            {
                ViewBag.Galla = 1;
            }
            ViewBag.TodayGalla = createGallaVM;


            if ((await _IBillSettingService.GetActiveAsync()) == null) {
                _notyf.Warning("No Active Bill setting. Contact your admin first...", 20);
            }

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
            ViewBag.TypeId = TypeId;
            ViewBag.TableId = TableId;
            return View(tableType);
        }
        public async Task<IActionResult> GetTable(Guid Id)
        {
            var table = (await _ITableService.GetAllAvailableAsync()).Where(x => x.Table_Type_Id == Id);
            if (table.Count() == 0)
            {
                _notyf.Error("No any table in this table type...", 5);
            }
            return PartialView("_TableList", model: table);
        }
        public async Task<IActionResult> GetOrder(Guid Id)
        {
            var order =await GetOrderVM(Id, "");
            ViewBag.Checkout = CreateCheckOut(order.Id, order.Order_Details.Sum(x => x.Total));
            return PartialView("_Order", model: order);
        }


        public async Task<IActionResult> CompleteCheckout(CreateCheckoutVM vm)
        {

            try
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
                    Paid_Amount = vm.Paid_Amount

                };
                var id = await _ICheckoutService.CreateAsync(dto).ConfigureAwait(true);
                var order = await _IOrderService.GetFirstOrDefaultByOrderIdAsync(vm.Order_Id).ConfigureAwait(true);
                await _ITableService.UpdateReservedAsync(order.Table_Id).ConfigureAwait(true);


                await SetCheckoutNotification(order.Table_Id).ConfigureAwait(true);
                foreach (var orderDetails in order.Order_Details)
                {
                    foreach (var menuIncredian in orderDetails.Menu.Menu_Incredians)
                    {
                        var inv = await _iInventoryService.GetInventoryByProductIdAsync(menuIncredian.Product_Id).ConfigureAwait(true);
                        if (inv != null)
                        {
                            var quantity = orderDetails.Quantity;
                            var menuQuantity = orderDetails.Menu.Menu_Details.Where(x => x.Id == orderDetails.Quantity_Id).FirstOrDefault().Divison.Value;
                            var incredianQuantity = menuIncredian.Quantity;
                            var stockQuantity = inv.StockQuantity - (quantity * menuQuantity * incredianQuantity);


                            var UpdateInventoryOnSale = new UpdateInventoryDTO()
                            {
                                Id = inv.Id,
                                StockQuantity = stockQuantity

                            };
                            await _iInventoryService.UpdateOnSaleAsync(UpdateInventoryOnSale).ConfigureAwait(true);
                        }

                    }


                }
                return RedirectToAction("InvoicePrint", "Invoice", new { area = "", Id = id, returnUrl = "/checkout" });
            }
            catch(Exception e)
            {
                _notyf.Error(e.Message);
                return Ok(false);
            }

         

        }

        public async Task<OrderVm> GetOrderVM(Guid tableId, string? orderNo)
        {
            var OrderVM = new OrderVm()
            {
                Order_Details = new List<OrderDetailVm>()
            };
            var order = new LotusRMS_Order();
            if (tableId != Guid.Empty)
            {
                OrderVM.TableId = tableId;
                OrderVM.Table_Name = (await _ITableService.GetByGuidAsync(tableId).ConfigureAwait(true)).Table_Name;
                order =await _IOrderService.GetFirstOrDefaultByTableIdAsync(tableId).ConfigureAwait(true);
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
                            Item_Name = menu.Item_Name + "(" + menu.Menu_Details.FirstOrDefault(x => x.Id == item.Quantity_Id).Divison.Title + ")",
                            Item_Unit = menu.Menu_Unit.Unit_Symbol,
                            Rate = item.Rate,
                            Remarks = item.Remarks,
                            Quantity = item.Quantity,
                            Quantity_Id=item.Quantity_Id,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete
                        };
                        OrderVM.Order_Details.Add(orderDetail);
                    }
                }
            }
            else if (orderNo != null)
            {
                order = await _IOrderService.GetFirstOrDefaultByOrderNoAsync(orderNo).ConfigureAwait(true);
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
                            Item_Name = menu.Item_Name + "(" + menu.Menu_Details.FirstOrDefault(x => x.Id == item.Quantity_Id).Divison.Title + ")",
                            Item_Unit = menu.Menu_Unit.Unit_Symbol,
                            Rate = item.Rate,
                            Remarks = item.Remarks,
                            Quantity = item.Quantity,
                            Quantity_Id=item.Quantity_Id,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete
                        };
                        OrderVM.Order_Details.Add(orderDetail);
                    }
                }
                OrderVM.TableId = order.Table_Id;
                OrderVM.Table_Name = order.Table.Table_Name;
            }
            return OrderVM;
        }

        public CreateCheckoutVM CreateCheckOut(Guid Order_Id,double Total)
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
        public async Task<IActionResult> GetAllCustomer()
        {
            var customer = (await _ICustomerService.GetAllAvailableAsync().ConfigureAwait(true)).Select(x=>new CustomerCheckOutVM()
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
        public double GetDue(List<LotusRMS_DueBook> dueBook)
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

        public async Task<IActionResult> PrintKOT(string OrderNo)
        {
            try
            {
                var order = await _IOrderService.GetFirstOrDefaultByOrderNoAsync(OrderNo).ConfigureAwait(true);
                var printOrderVM = new PrintOrderDetailVM()
                {
                    OrderNo = OrderNo,
                    TableName = order.Table.Table_Name,
                    OrderDetail = new List<OrderDetailVm>()
                };
                if (order.Order_Details.Any(x => !x.IsPrinted))
                {

                    foreach (var item in order.Order_Details.Where(x => !x.IsPrinted))
                    {
                        var menu = await _IMenuService.GetFirstOrDefaultByIdAsync(item.MenuId).ConfigureAwait(true);
                        var orderDetail = new OrderDetailVm()
                        {
                            Id = item.Id,
                            MenuId = item.MenuId,
                            Item_Name = menu.Item_Name + "(" + menu.Menu_Details.FirstOrDefault(x => x.Id == item.Quantity_Id).Divison.Title + ")",
                            Item_Unit = menu.Menu_Unit.Unit_Symbol,
                            Rate = item.Rate,
                            Quantity = item.Quantity,
                            Quantity_Id = item.Quantity_Id,
                            IsComplete = item.IsComplete,
                            IsPrinted = item.IsPrinted,
                            IsKitchenComplete = item.IsKitchenComplete
                        };
                        printOrderVM.OrderDetail.Add(orderDetail);

                    }
                }
                else
                {
                    foreach (var item in order.Order_Details)
                    {
                        var menu = await _IMenuService.GetFirstOrDefaultByIdAsync(item.MenuId).ConfigureAwait(true);
                        var orderDetail = new OrderDetailVm()
                        {
                            Id = item.Id,
                            MenuId = item.MenuId,
                            Item_Name = menu.Item_Name + "(" + menu.Menu_Details.FirstOrDefault(x => x.Id == item.Quantity_Id).Divison.Title + ")",
                            Item_Unit = menu.Menu_Unit.Unit_Symbol,
                            Rate = item.Rate,
                            Quantity = item.Quantity,
                            Quantity_Id = item.Quantity_Id,
                            IsComplete = item.IsComplete,
                            IsPrinted = item.IsPrinted,
                            IsKitchenComplete = item.IsKitchenComplete
                        };
                        printOrderVM.OrderDetail.Add(orderDetail);

                    }
                }
                return View(printOrderVM);
            }catch(Exception e)
            {
                _notyf.Error(e.Message);
                return Ok(false);
            }
        }
        public async Task<IActionResult> PrintKOTComplete(string OrderNo)
        {
            try
            {
                var order = await _IOrderService.GetFirstOrDefaultByOrderNoAsync(OrderNo).ConfigureAwait(true);
                var printOrderVM = new PrintOrderDetailVM()
                {
                    OrderNo = OrderNo,
                    TableName = order.Table.Table_Name,
                    OrderDetail = new List<OrderDetailVm>()
                };
                List<LotusRMS_Order_Details> orderDetails = new List<LotusRMS_Order_Details>();
                foreach (var item in order.Order_Details.Where(x => !x.IsPrinted))
                {

                    orderDetails.Add(item);

                }
                var id = await _IOrderService.PrintKotAsync(order.Id, orderDetails).ConfigureAwait(true);

                return Ok(true);
            }catch(Exception e)
            {
                _notyf.Error(e.Message);
                return Ok(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CompleteOrderDetail(string orderNo, Guid OrderDetailId)
        {
            var id = await _IOrderService.CompleteOrderDetailAsync(orderNo, OrderDetailId).ConfigureAwait(true);
            var order =await GetOrderVM(new Guid(), orderNo).ConfigureAwait(true);
           await SetOrderNotification(order.TableId).ConfigureAwait(true);

            ViewBag.Checkout = CreateCheckOut(order.Id, order.Order_Details.Sum(x => x.Total));
            return PartialView("_Order", model: order);
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder(string orderNo, Guid OrderDetailId)
        {

            var id = await _IOrderService.CancelOrderAsync(orderNo, OrderDetailId).ConfigureAwait(true);
            var order = await GetOrderVM(new Guid(), orderNo).ConfigureAwait(true);

            await SetOrderNotification(order.TableId).ConfigureAwait(true);

            ViewBag.Checkout = CreateCheckOut(order.Id, order.Order_Details.Sum(x => x.Total));
            return PartialView("_Order", model: order);
        }   

        public async Task<IActionResult> GetSwitchTableView(Guid TableId)
        {
            var typeTable = new List<TypeTableVM>();
            var type = (await _ITableTypeService.GetAllAvailableAsync().ConfigureAwait(true)).Select(t=>new TypeVM()
            {
                Id=t.Id,
                Type_Name=t.Type_Name
            });
            foreach(var item in type)
            {
                var tables = (await _ITableService.GetAllByTypeIdAsync(item.Id).ConfigureAwait(true)).Select(tab => new TableVM() {
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

        public async Task<IActionResult> SwitchTable(Guid OldTable,Guid NewTable)
        {
            var order = await _IOrderService.GetFirstOrDefaultByTableIdAsync(OldTable).ConfigureAwait(true);
            order.Table_Id = NewTable;

            var status = await _ITableService.UpdateReservedAsync(OldTable);
            var newStatus =await _ITableService.UpdateReservedAsync(NewTable);

            return RedirectToAction(nameof(Index));
        }
        #endregion
        public async Task SetCheckoutNotification(Guid Table_Id)
        {
            var typeId = (await _ITableService.GetFirstOrDefaultByIdAsync(Table_Id).ConfigureAwait(true)).Table_Type_Id;
            var tableBooked = (await _ITableService.GetAllByTypeIdAsync(typeId).ConfigureAwait(true)).Count(x => x.IsReserved);
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
            var typeId = (await _ITableService.GetFirstOrDefaultByIdAsync(Table_Id).ConfigureAwait(true)).Table_Type_Id;
            var tableBooked = (await _ITableService.GetAllByTypeIdAsync(typeId).ConfigureAwait(true)).Count(x => x.IsReserved);
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
