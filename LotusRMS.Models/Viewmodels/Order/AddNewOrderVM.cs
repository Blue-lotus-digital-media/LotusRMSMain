using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Order
{
    public class AddNewOrderVM
    {
        public Guid MenuId { get; set; }
        public virtual LotusRMS_Menu Menu { get; set; }
        public Guid TableId { get; set; }
        public string Item_Name { get; set; }
        public string? Item_Unit { get; set; }
        public double Quantity { get; set; }
        public Guid Quantity_Id { get; set; }
        public double Rate { get; set; }
        public string? Remarks { get; set; }  
        public double Total
        {
            get { return Quantity * Rate; }
        }


    }
}
