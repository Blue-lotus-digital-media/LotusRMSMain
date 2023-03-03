using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Type
{
    public class TypeVM
    {
        public Guid Id { get; set; }
        public string Type_Name { get; set; }
        public string Type_Description { get; set; }
        public bool Status { get; set; }

    }
}
