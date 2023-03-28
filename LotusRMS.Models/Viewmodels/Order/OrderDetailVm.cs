using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Order
{
    public class OrderDetailVm
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public Guid TableId { get; set; }
        public string Item_Name { get; set; }
        public string Item_Unit { get; set; }
        public float Quantity { get; set; }
        public float Rate { get; set; }
        private float total { get; set; }
        public bool IsComplete { get; set; }
        public bool IsKitchenComplete { get; set; }
        public string Remarks { get; set; }

        public float Total
        {
            get { return total; }
            set { total = Quantity * Rate; }
        }
    }
}
