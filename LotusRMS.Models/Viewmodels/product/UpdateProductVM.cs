using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.product
{
    public class UpdateProductVM:CreateProductVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
    }
}
