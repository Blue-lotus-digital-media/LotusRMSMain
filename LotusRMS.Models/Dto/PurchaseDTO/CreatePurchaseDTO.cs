using LotusRMS.Models.Viewmodels.Purchase;
using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.PurchaseDTO
{
    public class CreatePurchaseDTO
    {

        public Guid Supplier_Id { get; set; }
        public string Bill_No { get; set; }
        public string Purchase_Date { get; set; }
        
        public float Bill_Amount { get; set; }
        public PaymentModeEnum Payment_Mode { get; set; }
        public DiscountTypeEnum Discount_Type { get; set; }
        public float Discount { get; set; }
        public float Paid_Amount { get; set; }
        public float Due_Amount { get; set; }
        
        public List<CreatePurchaseDetailDTO> PurchaseDetails { get; set; }

    }
}
