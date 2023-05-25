using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
