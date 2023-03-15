using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string Invoice_String { get; set; }
        public int Invoice_No { get; set; }
        public int Print_Count { get; set; }
        public Guid CheckoutId { get; set; }
        [ForeignKey("Checkout_Id")]
        public virtual LotusRMS_Checkout Checkout { get; set; }
        public Guid FiscalYear_Id { get; set; }

        [ForeignKey("FiscalYear_Id")]
        public LotusRMS_FiscalYear FiscalYear { get; set; }

        public Guid BillSetting { get; set; }

    }
}
