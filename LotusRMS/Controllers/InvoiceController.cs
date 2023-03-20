using DocumentFormat.OpenXml.Drawing.Charts;
using LotusRMS.Models;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Checkout;
using LotusRMS.Models.Viewmodels.Invoice;
using LotusRMS.Models.Viewmodels.Order;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICompanyService _iCompanyService;
        private readonly ICustomerService _iCustomerService;
        private readonly IMenuService _iMenuService;

        public InvoiceController(IInvoiceService invoiceService, ICompanyService iCompanyService, ICustomerService iCustomerService, IMenuService iMenuService)
        {
            _invoiceService = invoiceService;
            _iCompanyService = iCompanyService;
            _iCustomerService = iCustomerService;
            _iMenuService = iMenuService;
        }

        public IActionResult InvoicePrint(Guid Id, string? returnUrl = null)
        {
            var invoice = _invoiceService.GetFirstOrDefault(Id);
            if (invoice.Checkout.Payment_Mode.ToString() == "Credit")
            {
                var due = _iCustomerService.GetFirstOrDefaultById((Guid)invoice.Checkout.Customer_Id);
                var dueAmount = due.DueBooks.FirstOrDefault(x => x.Invoice_Id == Id).BalanceDue;
                ViewBag.DueAmount = dueAmount;
            }
            var invoiceVM = new InvoiceVM()
            {
                Id = invoice.Id,
                Invoice_No = invoice.Invoice_No,
                Invoice_String = invoice.Invoice_String,
                Print_Count = invoice.Print_Count,
                FiscalYear_Id = invoice.FiscalYear_Id,
                FiscalYear = invoice.FiscalYear,
                BillSetting_Id = invoice.BillSetting_Id,
                BillSetting = invoice.BillSetting,
                Checkout_Id = invoice.Checkout_Id,
                Checkout = GetCheckout(invoice.Checkout)




            };


            ViewBag.Company = _iCompanyService.GetCompany();
             
            ViewBag.ReturnUrl = returnUrl;
            return View(invoiceVM);
        }

        public CheckoutVM GetCheckout(LotusRMS_Checkout checkout)
        {
            var checkouts = new CheckoutVM()
            {
                Id = checkout.Id,
                DateTime=checkout.DateTime,
                Total = checkout.Total,
                Customer_Id = checkout.Customer_Id,
                Customer_Address = checkout.Customer_Address,
                Customer_Name = checkout.Customer_Name,
                Customer_Contact = checkout.Customer_Contact,
                Paid_Amount = checkout.Paid_Amount,
                Discount = checkout.Discount,
                Discount_Type = checkout.Discount_Type.ToString(),
                Payment_Mode = checkout.Payment_Mode.ToString(),
                Order_Id = checkout.Order_Id,
                Order = GetOrderVM(checkout.Order)


            };
            return checkouts;

        }
        public OrderVm GetOrderVM(LotusRMS_Order order)
        {
            var OrderVM = new OrderVm()
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
                var menu = _iMenuService.GetFirstOrDefault(item.MenuId);
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
            return OrderVM;
        }
    }
}
