using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Menu
{
    public class MenuIncredianVM
    {
        public Guid Product_Id { get; set; }
        public string? Product_Name { get; set; }
        public string? Product_Unit { get; set; }
        public Guid Product_Unit_Id { get; set; }
        public double Quantity { get; set; }
        public string? Description { get; set; }

    }
    public class UpdateMenuIncredianVM:MenuIncredianVM
    {
        public Guid Id { get; set; }

    }
}
