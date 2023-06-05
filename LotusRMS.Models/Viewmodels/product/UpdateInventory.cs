using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.product
{
    public class UpdateInventory:CreateInventory
    {
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }

        public bool IsPurchased { get; set; }


    }
}
