using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Supplier
{
    public class CreateSupplierVM
    {
        [Required(ErrorMessage ="Full Name Required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact Required")]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Contact1 { get; set; }
        public string? PanOrVat { get; set; }
    }
}
