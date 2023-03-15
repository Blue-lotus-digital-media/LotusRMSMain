using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Checkout
{
    public class CreateCheckoutVM
    {
        public Guid Order_Id { get; set; }
        public Guid? Customer_Id { get; set; }
        public float Total { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Contact { get; set; }
        public float Paid_Amount { get; set; } 
        public DiscountTypeEnum Discount_Type { get; set; }
        public float Discount { get; set; }
        public PaymentModeEnum Payment_Mode { get;set; }
        

    }
}
