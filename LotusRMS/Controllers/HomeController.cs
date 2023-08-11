﻿using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.DataAccess.Constants;
using LotusRMS.Models;
using LotusRMS.Models.Service;
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
        private readonly IEmailSender _emailSender;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<RMSUser> userManager,
            IEmailSender emailSender,
            INotyfService notyf)
        {
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _notyf = notyf;
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