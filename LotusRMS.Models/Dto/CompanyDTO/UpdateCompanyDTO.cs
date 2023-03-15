using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CompanyDTO
{
    public class UpdateCompanyDTO:CreateCompanyDTO
    {
        public Guid Id { get; set; }
    }
}
