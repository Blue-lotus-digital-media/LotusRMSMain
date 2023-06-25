using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.Helper;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Company;
using Org.BouncyCastle.Utilities.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace LotusRMS.Models.Service.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
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

        public async Task<Guid> CreateAsync(CreateCompanyDTO obj)
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
            registrationNo: obj.RegistrationNo
            )
            {
                ContactPersons = obj.ContactPersons,
                IpV4Address="127.0.0.1"
            };
            await _companyRepository.AddAsync(Company).ConfigureAwait(false);
            await _companyRepository.SaveAsync().ConfigureAwait(false);
            scope.Complete();
            return Company.Id;

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
