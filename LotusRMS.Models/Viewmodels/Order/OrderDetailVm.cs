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
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public bool IsComplete { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsKitchenComplete { get; set; }
        public string Remarks { get; set; }
        public Guid Quantity_Id { get; set; }
        public Guid? Unit_Id { get; set; }
        public string? Quantity_Text { get; set; }
        public double Total
        {
            get { return Quantity * Rate; }
        }
    }
}
