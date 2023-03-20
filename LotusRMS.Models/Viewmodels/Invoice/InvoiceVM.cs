using LotusRMS.Models.Viewmodels.Checkout;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Invoice
{
    public class InvoiceVM
    {
        public Guid Id { get; set; }
        public string Invoice_String { get; set; }
        public int Invoice_No { get; set; }
        public int Print_Count { get; set; } = 1;
        public Guid Checkout_Id { get; set; }
        public CheckoutVM Checkout { get; set; }
        public Guid FiscalYear_Id { get; set; }

        public virtual LotusRMS_FiscalYear FiscalYear { get; set; }

        public Guid BillSetting_Id { get; set; }
        [ForeignKey("BillSetting_Id")]
        public virtual LotusRMS_BillSetting BillSetting { get; set; }
    }
}
