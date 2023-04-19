using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_MenuDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public bool Default { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDelete { get; set; } = false;


    }
}
