using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.SuperAdmin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
