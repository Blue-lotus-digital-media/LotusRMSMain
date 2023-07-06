using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
