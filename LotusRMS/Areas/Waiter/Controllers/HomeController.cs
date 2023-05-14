using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Waiter.Controllers
{
    [Area("Waiter")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
