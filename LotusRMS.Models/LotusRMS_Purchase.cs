using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Purchase
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid Supplier_Id { get; set; }
        [ForeignKey("Supplier_Id")]
        public virtual LotusRMS_Supplier Supplier { get; set; }
        public string? Bill_No { get; set; }
        public double Bill_Amount { get; set; }
        public DiscountTypeEnum Discount_Type { get; set; }
        public double Discount { get; set; }
        public PaymentModeEnum Payment_Mode { get; set; }
        public double Paid_Amount { get; set; }
        public double? Due { get; set; }
        public List<LotusRMS_PurchaseDetail> PurchaseDetails { get; set; }
        }
    public class LotusRMS_PurchaseDetail
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public virtual LotusRMS_Product Product { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double Total
        {
            get { return Quantity * Rate; }
        }

    }
}
