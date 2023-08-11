using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.SalesReport
{
    public class SalesReportByAllItem
    {
        public Guid Id { get; set; }
        public string Item_Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public double TotalSold { get; set; }
        public string Unit { get; set; }
        public string Unit_Division { get; set; }
    }
}
