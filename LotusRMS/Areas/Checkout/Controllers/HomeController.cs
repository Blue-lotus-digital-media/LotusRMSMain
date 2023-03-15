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
        public HomeController(IOrderService iOrderService, ITableTypeService TableTypeService, ITableService iTableService, IMenuService iMenuService, ICheckoutService iCheckoutService, UserManager<RMSUser> userManager)
        {
            _IOrderService = iOrderService;
            _ITableTypeService = TableTypeService;
            _ITableService = iTableService;
            _IMenuService = iMenuService;
            _UserManager = userManager;
            _ICheckoutService = iCheckoutService;
        }
        public IActionResult Index()
        {
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
                Payment_Mode = vm.Payment_Mode

            };
            var id=_ICheckoutService.Create(dto);



            return View();
        }

        public OrderVm GetOrderVM(Guid? tableId, string? orderNo)
        {
            var OrderVM = new OrderVm()
            {

                Order_Details = new List<OrderDetailVm>()
            };

            if (tableId != Guid.Empty)
            {
                var order = _IOrderService.GetFirstOrDefaultByTableId((Guid)tableId);
                if (order != null)
                {
                    OrderVM = new OrderVm()
                    {
                        Id = order.Id,
                        TableId = order.Table_Id,
                        Table_Name = order.Table.Table_Name,
                        OrderBy=order.User.FirstName +" "+ order.User.MiddleName +" "+ order.User.LastName,
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
                            Item_Name = menu.Item_Name + " (" + menu.Menu_Unit.Unit_Symbol + " )",
                            Rate = item.Rate,
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
                var order = _IOrderService.GetFirstOrDefaultByOrderNo(orderNo);
                if (order != null)
                {
                    OrderVM = new OrderVm()
                    {
                        Id = order.Id,
                        TableId = order.Table_Id,
                        Table_Name = order.Table.Table_Name,
                        OrderBy = order.User.FirstName + " " + order.User.MiddleName + " " + order.User.LastName,
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
                            Item_Name = menu.Item_Name + " (" + menu.Menu_Unit.Unit_Symbol + " )",
                            Rate = item.Rate,
                            Quantity = item.Quantity,
                            IsComplete = item.IsComplete,
                            IsKitchenComplete = item.IsKitchenComplete,
                            Total = item.GetTotal


                        };
                        OrderVM.Order_Details.Add(orderDetail);

                    }
                }

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
    }
}
