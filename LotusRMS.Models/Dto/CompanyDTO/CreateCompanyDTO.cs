using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CompanyDTO
{
    public class CreateCompanyDTO
    {
        public string CompanyName { get;  set; }
        public string Email { get;  set; }
        public string Country { get;  set; }
        public string City { get;  set; }
        public string Province { get;  set; }
        public string Tole { get;  set; }


        public string Contact { get;  set; }

        public List<ContactPerson> ContactPersons { get; set; }
        public string PanOrVat { get;  set; }

        public string RegistrationDate { get;  set; }
        public string CompanyRegistrationNumber { get; set; }
        public string ContractDate { get;  set; }
        public string ServiceStartDate { get;  set; }
        public string ValidTill { get;  set; }
        public string RegistrationNo { get; set; }
    }
}
