using Microsoft.AspNetCore.Mvc;

namespace LotusRMS.Areas.SuperAdmin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
