using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.FiscalYear
{
    public class FiscalYearVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StartDateAD { get; set; }
        public string StartDateBS { get; set; }
        public string EndDateAD { get; set; }
        public string EndDateBS { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }

    }
}
