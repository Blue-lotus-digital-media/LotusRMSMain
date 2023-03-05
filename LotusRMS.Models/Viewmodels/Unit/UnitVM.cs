using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Unit
{
    public class UnitVM
    {
        public Guid Id { get; set; }
        public string Unit_Name { get; set; }
        public string Unit_Symbol { get; set; }
        public string Unit_Description { get; set; }
        public bool Status { get; set; }
    }
}
