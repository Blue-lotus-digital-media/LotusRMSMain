using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.InventoryDTO
{
    public class CreateInventoryDTO
    {
        public Guid ProductId { get; set; }
        public double StockQuantity { get; set; }
        public double ReorderLevel { get; set; }
    }
}
