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
        public HomeController(IOrderService iOrderService, ITableTypeService TableTypeService, ITableService iTableService, IMenuService iMenuService, ICheckoutService iCheckoutService, UserManager<RMSUser> userManager, IBillSettingService iBillSettingService, ICustomerService iCustomerService)
        {
            _IOrderService = iOrderService;
            _ITableTypeService = TableTypeService;
            _ITableService = iTableService;
            _IMenuService = iMenuService;
            _UserManager = userManager;
            _ICheckoutService = iCheckoutService;
            _IBillSettingService = iBillSettingService;
            _ICustomerService = iCustomerService;
        }
        public IActionResult Index()
        {
            if (_IBillSettingService.GetActive() == null) {
                
            }

            var type = _ITableTypeService.GetAll().Where(x => !x.IsDelete && x.Status);
            return View(type);
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


        public IActionResult CompleteCheckout(CreateCheckoutVM vm)
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
        public IActionResult CompleteOrderDetail(string orderNo, Guid OrderDetailId)
        {
            var id = _IOrderService.CompleteOrderDetail(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);
            return PartialView("_Order", model: order);
        }
        [HttpPost]
        public IActionResult CancelOrder(string orderNo, Guid OrderDetailId)
        {

            var id = _IOrderService.CancelOrder(orderNo, OrderDetailId);
            var order = GetOrderVM(new Guid(), orderNo);
            return PartialView("_Order", model: order);
        }
        #endregion
    }
}
