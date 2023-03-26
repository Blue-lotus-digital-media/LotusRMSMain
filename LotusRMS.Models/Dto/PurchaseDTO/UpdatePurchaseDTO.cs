using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.PurchaseDTO
{
    public class UpdatePurchaseDTO:CreatePurchaseDTO
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
    }
}
