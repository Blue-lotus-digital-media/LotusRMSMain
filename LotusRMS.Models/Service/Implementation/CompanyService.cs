using AutoMapper;
using LotusRMS.Models.Dto.BillSettingDTO;
using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Helper;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LotusRMS.Models.Service.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<RMSUser> _userManager;
        private readonly IUserStore<RMSUser> _userStore;
        private readonly IUserEmailStore<RMSUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<CompanyService> _logger;
        public CompanyService(ICompanyRepository companyRepository,
UserManager<RMSUser> userManager,
IUserStore<RMSUser> userStore,
RoleManager<IdentityRole> roleManager,
ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _roleManager = roleManager;
            _logger = logger;
        }
        private IUserEmailStore<RMSUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<RMSUser>)_userStore;
        }
        public async Task<UpdateCompanyVM?> GetCompanyAsync()
        {
            var company = await _companyRepository.GetFirstOrDefaultAsync(includeProperties: "ContactPersons").ConfigureAwait(false);
            var upcVM = new UpdateCompanyVM()
            {
                ContactPerson = new List<UpdateContactPersonVM>()
            };
            if (company != null)
            {
                upcVM = new UpdateCompanyVM()
                {
                    Id = company.Id,
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
                    ContactPerson = new List<UpdateContactPersonVM>()
                };
                foreach (var item in company.ContactPersons)
                {
                    var ucp = new UpdateContactPersonVM()
                    {
                        Id = item.Id,
                        PersonName = item.PersonName,
                        Address = item.Address,
                        ContactNumber = item.ContactNumber,
                    };
                    upcVM.ContactPerson.Add(ucp);
                }
            }
            return upcVM;
        }

        public async Task<RMSUser> CreateAsync(CreateCompanyDTO obj)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var Company = new LotusRMS_Company(
            companyName: obj.CompanyName,
            country: obj.Country,
            province: obj.Province,
            city: obj.City,
            tole: obj.Tole,
            email: obj.Email,
            contact: obj.Contact,
            panOrVat: obj.PanOrVat,
            registrationDate: obj.RegistrationDate,
            validTill: obj.ValidTill,
            companyRegistrationNumber: obj.CompanyRegistrationNumber,
            contractDate: obj.ContractDate,
            serviceStartDate: obj.ServiceStartDate,
            registrationNo: obj.RegistrationNo)
            {
                ContactPersons = obj.ContactPersons,
                IpV4Address="127.0.0.1"
            };
            await _companyRepository.AddAsync(Company).ConfigureAwait(false);
            await _companyRepository.SaveAsync().ConfigureAwait(false);
            _logger.LogInformation("Company registered ");
 
             var user= await CompanyUserAsync(obj);
            
            scope.Complete();
            return user;

        }
        private async Task<RMSUser> CompanyUserAsync(CreateCompanyDTO request)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var userCheck = await _userManager.FindByEmailAsync(request.Email);

            if (userCheck == null)
            {
                _logger.LogInformation("user checked");
                var user = new RMSUser(firstName: request.CompanyName, middleName: "", lastName: "", contact: request.Contact)
                {

                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                };
                await _userStore.SetUserNameAsync(user, request.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user);
                var role = _roleManager.FindByNameAsync("Admin").Result;


                if (result.Succeeded)
                {
                    
                    await _userManager.AddToRoleAsync(user, role.ToString());
                    scope.Complete();
                    _logger.LogInformation("User created a new account with password. in company");

                    return user;

                   
                  

                }
                else
                {
                    throw new Exception("Cannot create user");
                }
            }
            else
            {
                throw new Exception("User with email already exist");
            }
        }


        public async Task<Guid> UpdateAsync(UpdateCompanyDTO obj)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var company = await _companyRepository.GetFirstOrDefaultAsync().ConfigureAwait(false);

            company.Update(
            companyName: obj.CompanyName,
            country: obj.Country,
            province: obj.Province,
            city: obj.City,
            tole: obj.Tole,
            email: obj.Email,
            contact: obj.Contact,
            contactPersons:obj.ContactPersons,
            panOrVat: obj.PanOrVat,
            registrationDate: obj.RegistrationDate,
            validTill: obj.ValidTill,
             companyRegistrationNumber: obj.CompanyRegistrationNumber,
            contractDate: obj.ContractDate,
            serviceStartDate: obj.ServiceStartDate,
            registrationNo:obj.RegistrationNo
                );
            await _companyRepository.UpdateAsync(company).ConfigureAwait(false);
            scope.Complete();
            return company.Id;
        }

        public async Task UpdateIpAsync(string Ip)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var company =await _companyRepository.GetFirstOrDefaultAsync().ConfigureAwait(false);
            company.IpV4Address = Ip;
            await _companyRepository.UpdateAsync(company).ConfigureAwait(false);
            scope.Complete();
        }
        public async Task<string> GetIpAsync()
        {

            var company = await _companyRepository.GetFirstOrDefaultAsync().ConfigureAwait(false);
            return company!=null? company.IpV4Address:"127.0.0.1";
        }

        public async Task<string> GetCompanyNameAsync()
        {
            var company = await _companyRepository.GetFirstOrDefaultAsync().ConfigureAwait(false);
            return company != null ? company.CompanyName : "No Name";
        }
    }
}
