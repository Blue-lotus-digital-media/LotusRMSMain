using LotusRMS.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class InvoiceReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
