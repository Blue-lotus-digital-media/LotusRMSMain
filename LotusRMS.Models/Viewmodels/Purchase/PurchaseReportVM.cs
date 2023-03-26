using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Purchase
{
    public class PurchaseReportVM
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Purchase_Date { get; set; }
        public float Bill_Amount { get; set; }
        public float Paid_Amount { get; set; }
        public float Discount { get; set; }
        public  string Discount_Type { get; set; }
        public  string Payment_Mode { get; set; }
        public int DetailCount { get; set; }
        public string Bill_No { get; set; }
        public string Supplier_Name { get; set; }

    }
}
