using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Inventory
{
    public class InventoryVm
    {
        public Guid Id { get; set; }
        public string Product_Name { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public double Stock_Quantity { get; set; }
        public double ReorderLevel { get; set; }
        public bool IsPurchased { get; set; }
    }
}
