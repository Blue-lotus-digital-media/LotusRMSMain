﻿using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.ExtendedProperties;
using LotusRMS.Models;
using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Identity;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Http;

namespace LotusRMSweb.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize(Roles = "SuperAdmin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;

        private readonly UserManager<RMSUser> _userManager;

        private readonly IEmailSender _emailSender;
        private readonly INotyfService _notyf;
        private readonly ILogger<CompanyController> _logger;
        public CompanyController(
            ICompanyService companyService,
            IUserService userService,
            INotyfService notyf,
            IEmailSender emailSender,
            UserManager<RMSUser> userManager,
            ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _userService = userService;
            _notyf = notyf;
            _emailSender = emailSender;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var company = await _companyService.GetCompanyAsync().ConfigureAwait(true);
            if (company.CompanyName == null)
            {
                return RedirectToAction(nameof(Create));
            }
           
            return View(company);
        }
        public IActionResult Create()
        {
            var createCompany = new CreateCompanyVM()
            {
                ContactPerson = new List<CreateContactPersonVM>()
                {
                    new CreateContactPersonVM(),
                    new CreateContactPersonVM()
                }
            };
            return View(createCompany);
        }
             
        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyVM company)
        {
            var dto = new CreateCompanyDTO() {
                CompanyName = company.CompanyName,
                Country = company.Country,
                Province = company.Province,
                City = company.City,
                Tole = company.Tole,
                Email = company.Email,
                Contact = company.Contact,
                PanOrVat = company.PanOrVat,
                RegistrationDate = company.RegistrationDate,
                RegistrationNo=company.RegistrationNo,
                WebSite=company.WebSite,
                CompanyRegistrationNumber = company.CompanyRegistrationNumber,
                ContractDate = company.ContractDate,
                ServiceStartDate = company.ServiceStartDate,
                ValidTill = company.ValidTill,
                ContactPersons = new List<ContactPerson>()
            };
            foreach (var item in company.ContactPerson)
            {
                var ucp = new ContactPerson(personName: item.PersonName,
                    address : item.Address,
                    contactNumber :item.ContactNumber
                );
            dto.ContactPersons.Add(ucp);
            }
            try
            {
                var user = await _companyService.CreateAsync(dto).ConfigureAwait(false);
                _logger.LogInformation("company user created successfully");
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { area = "", userId = user.Id, token = code }, Request.Scheme);
                await _emailSender.SendEmailAsync(new Message(new string[] { user.Email }, "Welcome to LotusRMS",
                    $"<h2>Welcome to the Blue Lotus Digital pvt. ltd. family. </h2>Please confirm your email by " +
                    $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>. to activate your LotusRMS account.<p>Thank you for choosing LotusRMS</p>", null));
            }
            catch (Exception e)
            {
                _notyf.Error(e.Message, 10);
            }
            return RedirectToAction(nameof(Index));
        }

      public async Task<IActionResult> Update()
        {
            var company =await _companyService.GetCompanyAsync().ConfigureAwait(true);
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCompanyVM company)
        {

            var dto = new UpdateCompanyDTO()
            {
                Id=company.Id,
                CompanyName = company.CompanyName,
                Country = company.Country,
                Province = company.Province,
                City = company.City,
                Tole = company.Tole,
                Email = company.Email,
                Contact = company.Contact,
                PanOrVat = company.PanOrVat,
                RegistrationDate = company.RegistrationDate,
                RegistrationNo=company.RegistrationNo,
                CompanyRegistrationNumber = company.CompanyRegistrationNumber,
                ContractDate = company.ContractDate,
                ServiceStartDate = company.ServiceStartDate,
                WebSite=company.WebSite,
                ValidTill = company.ValidTill,
                ContactPersons = new List<ContactPerson>()
            };
            foreach (var item in company.ContactPerson)
            {
                var ucp = new ContactPerson(

                    personName: item.PersonName,
                    address: item.Address,
                    contactNumber: item.ContactNumber
                )
                {
                    Id = item.Id
                };
                dto.ContactPersons.Add(ucp);

            }
            var id = await _companyService.UpdateAsync(dto).ConfigureAwait(true);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            var byteArray = ImageUpload.GetByteArrayFromImage(file);
            await _companyService.UpdateLogoAsync(byteArray).ConfigureAwait(true);


            return RedirectToAction("Index");
        }
    }
}
