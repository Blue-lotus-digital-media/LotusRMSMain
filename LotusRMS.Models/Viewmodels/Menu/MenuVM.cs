using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Menu
{
    public class MenuVM
    {
        public Guid Id { get; set; }
        public string Item_name { get; set; }
        public float Rate { get; set; }
        public float Unit_Quantity { get; set; }
        public string OrderTo { get; set; }
        public string Menu_Image { get; set; }
        public string Menu_Unit_Name { get; set; }
        public string Menu_Category_Name { get; set; }
        public string Menu_Type_Name { get; set; }
        public List<MenuDetailVM> MenuDetail { get; set; }
        public bool Status { get; set; }
    }
    public class MenuDetailVM
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public double Rate { get; set; }
        public bool IsDefault { get; set; }

    }
}
