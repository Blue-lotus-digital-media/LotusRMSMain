using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_FiscalYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StartDateAD { get; set; }
        public string StartDateBS { get; set; }
        public string EndDateAD { get; set; }
        public string EndDateBS { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}
