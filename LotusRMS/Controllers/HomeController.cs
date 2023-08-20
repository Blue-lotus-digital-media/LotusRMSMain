using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.DataAccess.Constants;
using LotusRMS.Models;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Company;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LotusRMSweb.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<RMSUser> _userManager;
        private readonly INotyfService _notyf;
        private readonly ICompanyService _companyService;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<RMSUser> userManager,
            IEmailSender emailSender,
            INotyfService notyf,
            ICompanyService companyService)
        {
            _logger = logger;
            _userManager = userManager;
            _notyf = notyf;
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
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
            }else if (User.IsInRole("Bar"))
            {
                return RedirectToAction("Index", "Home", new { area = "Order" });
            }

            var company = await _companyService.GetCompanyAsync().ConfigureAwait(true);
            if (company == null)
            {
                company = new UpdateCompanyVM(){
                    CompanyName= "Blue Lotus digital pvt ltd.",
                    City="Birtamode",
                    Tole="Athitisadan",
                    Contact="9844662120",
                    Email="bluelotusdigital.bld@gmail.com" 
                };
            }
            return View(company);
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