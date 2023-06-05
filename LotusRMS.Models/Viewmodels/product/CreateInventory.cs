using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.product
{
    public class CreateInventory
    {
        [Required(ErrorMessage ="Stock Quantity Required!")]
        public double StockQuantity { get; set; }

        [Required(ErrorMessage = "Reorder Level Required!")]
        public double ReorderLevel { get; set; }
    }
}
