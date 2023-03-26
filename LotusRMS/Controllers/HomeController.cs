using LotusRMS.DataAccess.Constants;
using LotusRMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LotusRMSweb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<RMSUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<RMSUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }else if (User.IsInRole("SuperAdmin"))
            {
                return RedirectToAction("Index", "Home", new { area = "SuperAdmin" });
            }else if (User.IsInRole("Kitchen"))
            {
                return RedirectToAction("Index", "Home", new { area = "Kitchen" });
            }else if (User.IsInRole("Waiter"))
            {
                return RedirectToAction("Index", "Home", new { area = "Order" });
            }else if (User.IsInRole("Cashier"))
            {
                return RedirectToAction("Index", "Home", new { area = "Checkout" });
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}