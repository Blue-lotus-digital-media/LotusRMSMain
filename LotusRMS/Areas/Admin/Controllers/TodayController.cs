using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin,SuperAdmin,Cashier,Waiter,Bar")]
    public class TodayController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
