using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Menu.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
