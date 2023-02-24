using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels
{
    public class UnitVM 
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Unit_Symbol { get; set; }
        [Required]
        public string? Unit_Description { get; set; }
        public bool Status { get; set; }



        
    }
}
