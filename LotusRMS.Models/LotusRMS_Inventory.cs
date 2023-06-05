using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Inventory
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        [ForeignKey(nameof(Product_Id))]
        public virtual LotusRMS_Product Product { get; set; }

        public double StockQuantity { get; set; }
        public double ReorderLevel { get; set; }
        public bool IsPurchased { get; set; } = false;
        public string? Remarks { get; set; }
    }
}
