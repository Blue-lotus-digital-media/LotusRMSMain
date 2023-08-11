using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.SalesReport
{
    public class SalesReportBySingleItem
    {
        public Guid Id { get; set; }
        public string Item_Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
    public class SalesReportBySingleItemDetail{
        public DateTime DateTime { get; set; }
        public double TotalSold { get; set; }
    }
}
