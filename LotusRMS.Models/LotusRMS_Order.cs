using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string Order_No { get; set; }
        public Guid Table_Id { get; set; }

        [Display(Name = "Date")]
        public string DateTime { get; set; }

        public string OrderBy { get; set; }
        [ForeignKey("OrderBy")]
        public virtual RMSUser User { get; set; }

        public List<LotusRMS_Order_Details> Order_Details { get;set; }
        public float Total { get; set; }
        public float Discount { get; set; }
        public bool IsCheckout { get; set; } = false;
        public bool Status { get; set; }






    }
}
