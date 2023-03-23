using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Supplier
{
    public class SupplierVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Contact1 { get; set; }
        public string PanOrVat { get; set; }
        public bool Status { get; set; }

    }
}
