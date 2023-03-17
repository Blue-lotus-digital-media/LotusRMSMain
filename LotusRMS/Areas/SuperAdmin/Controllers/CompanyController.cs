using DocumentFormat.OpenXml.ExtendedProperties;
using LotusRMS.Models;
using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize(Roles = "SuperAdmin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            var company = _companyService.GetCompany();
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
        public IActionResult Create(CreateCompanyVM company)
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
            var id=_companyService.Create(dto);

            return RedirectToAction(nameof(Index));
        }

      public IActionResult Update()
        {
            var company = _companyService.GetCompany();
            return View(company);
        }

        [HttpPost]
        public IActionResult Update(UpdateCompanyVM company)
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
            var id = _companyService.Update(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
