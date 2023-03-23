using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.SupplierDTO
{
    public class UpdateSupplierDTO:CreateSupplierDTO
    {
        public Guid Id { get; set; }
    }
}
