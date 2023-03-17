using LotusRMS.Models.Dto.DueBookDTO;
using LotusRMS.Models.Viewmodels.DueBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Customer
{
    public class CreateCustomerVM
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string? PanOrVat { get; set; }
        public float DueAmount { get; set; }
    }
}
