using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.FiscalYear
{
    public class CreateFiscalYearVM
    {
        [Required(ErrorMessage ="Fiscal year name required")]
        public string Name { get; set; }
       
        public string StartDateAD { get; set; }
        [Required(ErrorMessage = "Start date (BS) required")]
        public string StartDateBS { get; set; }
      
        public string EndDateAD { get; set; }
        [Required(ErrorMessage = "End date (BS) required")]
        public string EndDateBS { get; set; }
        public bool IsActive { get; set; }
    }
}
