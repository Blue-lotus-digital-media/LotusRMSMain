using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.SupplierDTO
{
    public class CreateSupplierDTO
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string? PanOrVat { get; set; }
        public string? Contact1 { get; set; }
    }
}
