using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Checkout
{
    public class PrintOrderDetailVM
    {
        public string OrderNo { get; set; }
        public string TableName { get; set; }
        public string Time
        {
            get
            {
                return CurrentTime.DateTimeNow().ToString();
            }
        }
        public List<OrderDetailVm> OrderDetail {get;set;}
    }
}
