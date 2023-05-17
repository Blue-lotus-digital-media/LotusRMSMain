using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Checkout
{
    public class CheckoutVM
    {
        public Guid Id { get; set; }
        public Guid Order_Id { get; set; }
        public OrderVm  Order{ get; set; }
        public DateTime DateTime { get; set; }
        public Guid? Customer_Id { get; set; }
        public double Total { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Contact { get; set; }
        public double Paid_Amount { get; set; }
        public string Discount_Type { get; set; }
        public float Discount { get; set; }
        public string Payment_Mode { get; set; }
    }
}
