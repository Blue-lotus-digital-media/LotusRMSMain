using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_BillSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string BillPrefix { get; set; }
        public string BillTitle { get; set; }
        public string BillAddress { get; set; }
        public string BillNote { get; set; }

        public bool IsPhone { get; set; }
        public bool IsPanOrVat { get; set; }
        public bool IsFiscalYear { get; set; }

        public bool IsActive { get; set; }
        public bool Status { get; set; }=true;
        public bool IsDelete { get; set; } = false;
    }
}
