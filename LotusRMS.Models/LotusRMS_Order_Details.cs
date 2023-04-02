using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Order_Details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public float Quantity { get; set; }
        public float Rate { get; set; }

        public string? Remarks { get; set; }
        private float Total { get; set; }
        public float GetTotal {
            get { return Total; }
            set { Total = Quantity * Rate; }
        }
        public bool IsComplete { get; set; } = false;
        public bool IsKitchenComplete { get; set; } = false;
        public bool IsQrOrder { get; set; } = false;
        public bool IsQrVerified { get; set; } = false;
        public bool IsPrinted { get; set; } = false;

        public Guid MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual LotusRMS_Menu Menu { get; set; }

        
    }
}
