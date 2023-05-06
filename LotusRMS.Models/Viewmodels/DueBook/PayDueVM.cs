using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.DueBook
{
    public class PayDueVM

    {
        public Guid CustomerId { get; set; }
        public LotusRMS_Customer? Customer { get; set; }
        public double DueAmount { get; set; }
        [Required(ErrorMessage ="Paid Amount required")]
        public double PaidAmount { get; set; }
        public double BalanceDue { get {return DueAmount-PaidAmount;} }
        public string Remarks { get; set; }

    }
}
