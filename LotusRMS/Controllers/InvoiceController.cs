using DocumentFormat.OpenXml.Drawing.Charts;
using LotusRMS.Models;
using LotusRMS.Models.Dto.GallaDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Checkout;
using LotusRMS.Models.Viewmodels.Galla;
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
        private readonly IGallaService _gallaService;
        private readonly IOrderService _orderService;
        private readonly IFiscalYearService _iFiscalYearService;
        private readonly IBillSettingService _iBillSettingService;

        public InvoiceController(IInvoiceService invoiceService,
            ICompanyService iCompanyService,
            ICustomerService iCustomerService,
            IMenuService iMenuService,
            IGallaService gallaService,
            IOrderService orderService,
            IFiscalYearService iFiscalYearService,
            IBillSettingService iBillSettingService)
        {
            _invoiceService = invoiceService;
            _iCompanyService = iCompanyService;
            _iCustomerService = iCustomerService;
            _iMenuService = iMenuService;
            _gallaService = gallaService;
            _orderService = orderService;
            _iFiscalYearService = iFiscalYearService;
            _iBillSettingService = iBillSettingService;
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
            var checkout = GetCheckout(invoice.Checkout);
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
                Checkout = checkout
              };


            var galla = _gallaService.GetTodayGalla();
            var gallaDetail = new CreateGallaDetailVM()
            {
                Title=  invoice.Invoice_String,
                Deposit = checkout.Paid_Amount,

                Withdrawl = checkout.Paid_Amount - checkout.Total + checkout.Discount,

            };

            var addGallaDetail = new AddGallaDetailDTO() {
                Galla_Id = galla.Id,
            GallaDetail = gallaDetail
            };

            _gallaService.AddGallaDetail(addGallaDetail);


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
                Date = order.DateTime.ToString(),
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
                    Item_Name = menu.Item_Name + "(" + menu.Menu_Details.FirstOrDefault(x => x.Id == item.Quantity_Id).Divison.Title + ")",

                    Item_Unit = menu.Menu_Unit.Unit_Symbol,
                    Rate = item.Rate,
                    Quantity = item.Quantity,
                    IsComplete = item.IsComplete,
                    IsKitchenComplete = item.IsKitchenComplete,
                    
                };
                OrderVM.Order_Details.Add(orderDetail);

            }
            return OrderVM;
        }
        public async Task<IActionResult> EstimateBillPrint(Guid Order_Id)
        {
            var fiscalyear =await _iFiscalYearService.GetActiveYearAsync();
            var billSetting = _iBillSettingService.GetActive();
            var order = _orderService.GetFirstOrDefaultByOrderId(Order_Id);
            var estimateVM = new EstimateInvoiceVM()
            {
                OrderId = order.Id,
                Order = GetOrderVM(order),
                FiscalYear = fiscalyear,
                BillSetting=billSetting
            };
            ViewBag.Company = _iCompanyService.GetCompany();
            return View(estimateVM);
        }

    }
}
