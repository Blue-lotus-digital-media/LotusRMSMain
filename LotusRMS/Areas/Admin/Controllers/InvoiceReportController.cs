using ClosedXML.Excel;
using LotusRMS.DataAccess.Constants;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Report;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class InvoiceReportController : Controller
    {
        private readonly IInvoiceService _iInvoiceService;

        public InvoiceReportController(IInvoiceService iInvoiceService)
        {
            _iInvoiceService = iInvoiceService;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APICalls
        [HttpGet]
        public IActionResult GetByDateRange(DateTime StartDate,DateTime EndDate)
        {
            EndDate=EndDate.AddDays(1).AddMilliseconds(-1);
            var invoice = _iInvoiceService.GetAllByDateRange(StartDate,EndDate).Select(inv=>new InvoiceReportVM()
            {
                Id=inv.Id,
                Invoice_No=inv.Invoice_String,
                Date=inv.Checkout.DateTime.ToString(),
                CustomerName=inv.Checkout.Customer_Name,
                Total=inv.Checkout.Total,
                Discount=inv.Checkout.Discount,
                DiscountType=inv.Checkout.Discount_Type.ToString(),
                Paid_Amount=inv.Checkout.Paid_Amount,
                PaymentMode=inv.Checkout.Payment_Mode.ToString()
            });
            return Json(new { data=invoice });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel(DateTime StartDate, DateTime EndDate)
        {
            EndDate = EndDate.AddDays(1).AddMilliseconds(-1);
            var invoice = _iInvoiceService.GetAllByDateRange(StartDate, EndDate).Select(inv => new InvoiceReportVM()
            {
                Id = inv.Id,
                Invoice_No = inv.Invoice_String,
                Date = inv.Checkout.DateTime.ToString(),
                CustomerName = inv.Checkout.Customer_Name,
                Total = inv.Checkout.Total,
                Discount = inv.Checkout.Discount,
                DiscountType = inv.Checkout.Discount_Type.ToString(),
                Paid_Amount = inv.Checkout.Paid_Amount,
                PaymentMode = inv.Checkout.Payment_Mode.ToString()
            });
          

            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(invoice.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Invoicereport-" + StartDate.ToShortDateString()+"-"+EndDate.ToShortDateString() + ".xlsx");
                }
            }
           
        }
        #endregion
    }
}
