using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.CustomerDTO
{
    public class UpdateCustomerDTO:CreateCustomerDTO
    {
        public Guid Id { get; set; }
    }
}
