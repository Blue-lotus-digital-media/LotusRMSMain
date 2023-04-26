using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Order
{
    public class OrderMenu
    {
        public Guid Id { get; set; }
        public  string Item_Name { get; set; }
        public double Rate { get; set; }
        public string Symbol { get; set; }
    }
}
