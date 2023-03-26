using LotusRMS.Models.Viewmodels.Supplier;
using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Purchase
{
    public class CreatePurchaseVm
    {
        public Guid? SupplierId { get; set; }
        public string? BillNo { get; set; }
        public string DateAD { get; set; }
        public string DateBS { get; set; }
        public float? BillTotal { get; set; }
        public PaymentModeEnum Payment_Mode { get; set; }
        public DiscountTypeEnum Discount_Type { get; set; }
        public float Discount { get; set; }
        public float Paid_Amount { get; set; }
        public float? Due_Amount { get; set; }
        [Required, MinLength(1, ErrorMessage = "At least one item required in Product List")]
        public List<CreatePurchaseDetailVM> ProductList { get; set; }





    }
}
