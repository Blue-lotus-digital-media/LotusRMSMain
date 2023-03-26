using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.PurchaseDTO
{
    public class CreatePurchaseDetailDTO
    {
        public Guid Product_Id { get; set; }
        public float Product_Quantity { get; set; }
        public float Product_Rate { get; set; }
        public float Total
        {
            get { return Product_Quantity * Product_Rate; }

        }
    }
}
