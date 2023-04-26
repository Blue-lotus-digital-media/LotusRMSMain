using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LotusRMS.Models.Dto.CheckoutDTO
{
    public class CreateCheckoutDTO
    {
        public Guid? Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Contact { get; set; }

        public Guid Order_Id { get; set; }
        public double Total { get; set; }
        public DiscountTypeEnum Discount_Type { get; set; }
        public float Discount { get; set; }
        public float Paid_Amount { get; set; }
        public PaymentModeEnum Payment_Mode { get; set; }
        [Display(Name = "Date")]
        public string DateTime { get; set; }

        public string Invoice_No { get; set; }
    }
}
