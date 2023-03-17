using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Company
{
    public class UpdateCompanyVM
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Tole { get; set; }


        public string Contact { get; set; }

        public List<UpdateContactPersonVM> ContactPerson { get; set; }
        public string PanOrVat { get; set; }

        public string RegistrationDate { get; set; }
        public string RegistrationNo { get; set; }
        public string ValidTill { get; set; }

        public string CompanyRegistrationNumber { get;  set; }

        public string ContractDate { get;  set; }
        public string ServiceStartDate { get;  set; }
    }
}
