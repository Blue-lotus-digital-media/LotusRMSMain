using LotusRMS.Utility.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Report
{
    public class InvoiceReportVM
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Invoice_No { get; set; }
        public string CustomerName { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public string DiscountType { get; set; }
        public double Paid_Amount { get; set; }
        public string PaymentMode { get; set; }

    }
}
