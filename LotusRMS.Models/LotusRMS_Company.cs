using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace LotusRMS.Models
{
    public class LotusRMS_Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string CompanyName { get;private set; }
        public string Email { get; private set; }
        public string Country { get; private set; }
        public string City { get;private set; }
        public string Province { get; private set; }
        public string Tole { get; private set; }


        public string Contact{ get; private set; }
        
        public List<ContactPerson> ContactPersons { get; set; }
        public string PanOrVat { get; private set; }

        public string RegistrationDate { get;private set; }
        public string RegistrationNo { get; private set; }
        public string CompanyRegistrationNumber { get; private set; }
        [MaybeNull]
        public string WebSite { get; set; }

        public string ContractDate { get; private set; }
        public string ServiceStartDate { get; private set; }
        public string ValidTill { get; private set; }
        public string IpV4Address { get; set; }
        [MaybeNull]
        public byte[] Logo { get; set; }

        public LotusRMS_Company(string companyName,
                                string country,
                                string province,
                                string city,
                                string tole,
                                string email,
                                string contact,
                                string panOrVat,
                                string registrationDate,
                                string validTill,
                                string companyRegistrationNumber,
                                string contractDate,
                                string serviceStartDate,
                                string registrationNo)
        {
            CompanyName = companyName;
            Country = country;
            Province = province;
            City = city;
            Tole = tole;
            Email = email;
            Contact = contact;
            PanOrVat = panOrVat;
            RegistrationDate = registrationDate;
            ValidTill = validTill;
            CompanyRegistrationNumber = companyRegistrationNumber;
            ContractDate = contractDate;
            ServiceStartDate = serviceStartDate;
            RegistrationNo = registrationNo;
        }

        public LotusRMS_Company(string companyName, string city, string tole, string contact, string email,string website)
        {
            CompanyName = companyName;
            City = city;
            Tole = tole;
            Contact = contact;
            Email = email;
            WebSite = website;
        }

        public void Update(string companyName,
                           string country,
                           string province,
                           string city,
                           string tole,
                           string email,
                           string contact,
                           string panOrVat,
                           string registrationDate,
                           string validTill,
                             string companyRegistrationNumber,
                                string contractDate,
                                string serviceStartDate,
                                string registrationNo
            )
        {
            CompanyName = companyName;
            Country = country;
            Province = province;
            City = city;
            Tole = tole;
            Email = email;
            Contact = contact;
            PanOrVat = panOrVat;
            RegistrationDate = registrationDate;
            ValidTill = validTill;

            CompanyRegistrationNumber = companyRegistrationNumber;
            ContractDate = contractDate;
            ServiceStartDate = serviceStartDate;

            RegistrationNo = registrationNo;
        }
    }
    public class ContactPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        [MaybeNull]
        public string PersonName { get;private set; }
        [MaybeNull]
        public string ContactNumber { get; private set; }
        [MaybeNull]
        public string Address { get; private set; }
        public ContactPerson(string personName,string contactNumber,string address)
        {
            PersonName = personName;
            ContactNumber = contactNumber;
            Address = address;
        }
        public void UpdateContactPerson(string personName, string contactNumber, string address)
        {
           
            PersonName = personName;
            ContactNumber = contactNumber;
            Address = address;
        }
    }
}
