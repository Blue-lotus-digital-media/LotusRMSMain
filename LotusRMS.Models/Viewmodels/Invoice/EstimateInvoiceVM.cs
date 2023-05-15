using LotusRMS.Models.Viewmodels.Checkout;
using LotusRMS.Models.Viewmodels.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Invoice
{
    public class EstimateInvoiceVM
    {
        public Guid OrderId { get; set; }
        public OrderVm Order { get; set; }
        public virtual LotusRMS_FiscalYear FiscalYear { get; set; }
        public virtual LotusRMS_BillSetting BillSetting { get; set; }


    }
}
