﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.PurchaseDTO
{
    public class CreatePurchaseDetailDTO
    {
        public Guid Product_Id { get; set; }
        public double Product_Quantity { get; set; }
        public double Product_Rate { get; set; }
        public double Total
        {
            get { return Product_Quantity * Product_Rate; }

        }
    }
}
