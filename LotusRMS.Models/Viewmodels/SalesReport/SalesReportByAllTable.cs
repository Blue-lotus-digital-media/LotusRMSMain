using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.SalesReport
{
    public class SalesReportByAllTable
    {
        public Guid Id { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public double GrandTotal { 
            get { return Total-Discount;} 
        }
    }
}
