using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Order
{
    public class OrderVm
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public string Order_No{get;set;}
        public string Date { get; set; }
        public List<OrderDetailVm> Order_Details { get; set; }
    }
}
