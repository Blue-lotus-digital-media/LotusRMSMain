using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class PurchaseReportController : Controller
    {
        private readonly IPurchaseService _iPurchaseService;

        public PurchaseReportController(IPurchaseService iPurchaseService)
        {
            _iPurchaseService = iPurchaseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Api Calls

        [HttpGet]
        public IActionResult GetByDateRange(string startDate, string endDate)
        {
            var purchase = _iPurchaseService.GetByDateRange(startDate, endDate).Select(det => new PurchaseReportVM()
            {
                Id = det.Id,
                Bill_Amount = det.Bill_Amount,
                Paid_Amount = det.Paid_Amount,
                Bill_No = det.Bill_No,
                Discount_Type = det.Discount_Type.ToString(),
                Discount = CheckDiscount(det.Discount_Type.ToString(), det.Discount) * det.Discount,
                Payment_Mode = det.Payment_Mode.ToString(),
                Date = det.Date.ToString(),
                Purchase_Date = det.PurchaseDate.ToShortDateString(),
                DetailCount = det.PurchaseDetails.Count(),
                Supplier_Name = det.Supplier.FullName
            });
            return Json(new { data = purchase });
        }

        private float CheckDiscount(string type, float discount)
        {
            if (type == "Cash")
            {
                return 1;
            }
            else
            {
                return discount / 100;
            }
        }

        #endregion
    }
}