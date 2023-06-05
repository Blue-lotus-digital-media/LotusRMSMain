using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.InventoryDTO
{
    public class UpdateInventoryDTO:CreateInventoryDTO
    {
        public Guid Id { get; set; }
        public bool IsPurchased { get; set; }
    }
}
