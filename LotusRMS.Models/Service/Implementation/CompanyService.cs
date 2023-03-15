﻿using LotusRMS.Models.Dto.CompanyDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Company;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
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

        public UpdateCompanyVM GetCompany()
        {
            var company = _companyRepository.GetFirstOrDefault(includeProperties: "ContactPersons");
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

        public Guid Create(CreateCompanyDTO obj)
        {
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
            registrationNo:obj.RegistrationNo

            )
            {
                ContactPersons=new List<ContactPerson>()
            };
            foreach(var item in obj.ContactPersons)
            {
                Company.ContactPersons.Add( item ); 
            }
            _companyRepository.Add(Company);
            _companyRepository.Save();
            return Company.Id;

        }

        public async Task<Guid> Update(UpdateCompanyDTO obj)
        {
            var company = _companyRepository.GetFirstOrDefault();

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
            _companyRepository.Update(company);
            return company.Id;
        }

     
    }
}