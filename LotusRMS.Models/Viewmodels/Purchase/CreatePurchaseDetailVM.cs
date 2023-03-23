﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Purchase
{
    public class CreatePurchaseDetailVM
    {
        public Guid Product_Id { get; set; }
        public string? Product_Name { get; set; }
        public float Product_Quantity { get; set; }
        public string? Product_Unit { get; set; }
        public float Product_Rate { get; set; }
        public float Total {
            get { return Product_Quantity * Product_Rate; }
            
        }
    }
}
