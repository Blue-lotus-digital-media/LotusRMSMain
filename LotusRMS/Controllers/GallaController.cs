using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    public class GallaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
