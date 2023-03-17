using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Checkout
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? Customer_Id { get;set; }
        public string Customer_Name { get; set; }
        public string? Customer_Address { get; set; }
        public string? Customer_Contact { get; set; }

        public Guid Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public virtual LotusRMS_Order Order { get; set; }

        public float Total { get; set; }
        public DiscountTypeEnum Discount_Type { get; set; }
        public float Discount { get; set; } 
        public float Paid_Amount { get; set; }
        public PaymentModeEnum Payment_Mode { get; set; }
        [Display(Name="Date")]
        public string DateTime { get; set; }

        public string Invoice_No { get; set; }



    }
}
