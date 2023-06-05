using LotusRMS.Models.Viewmodels.Supplier;
using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required(ErrorMessage ="Date is not selected")]
        [DisplayName("Date")]
        public string DateBS { get; set; }
        public double BillTotal { get; set; }
        public PaymentModeEnum Payment_Mode { get; set; }
        public DiscountTypeEnum Discount_Type { get; set; }
        public double Discount { get; set; }
        public double Paid_Amount { get; set; }
        public double? Due_Amount { get; set; }
        
        [Required(ErrorMessage = "Product list must contain 1 product")]
        [DisplayName("Product List")]
        public List<CreatePurchaseDetailVM> ProductList { get; set; }





    }
}
