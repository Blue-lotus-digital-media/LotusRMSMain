using LotusRMS.DataAccess.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LotusRMSweb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SalesReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
