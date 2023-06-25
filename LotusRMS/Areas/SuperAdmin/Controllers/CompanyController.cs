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
    [Authorize(Roles = "superadmin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
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
            var id=await _companyService.CreateAsync(dto).ConfigureAwait(true);

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
    }
}
