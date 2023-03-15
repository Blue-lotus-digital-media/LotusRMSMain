using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.FiscalYearDTO
{
    public class CreateFiscalYearDTO
    {
        public string Name { get; set; }
        public string StartDateAD { get; set; }
        public string StartDateBS { get; set; }
        public string EndDateAD { get; set; }
        public string EndDateBS { get; set; }
        public bool IsActive { get; set; } = true;

      
    }
}
