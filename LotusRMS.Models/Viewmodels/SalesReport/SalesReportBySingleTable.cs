using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.SalesReport
{
    public class SalesReportBySingleTable
    {
        public Guid Id { get; set; }
        public string Table_Name { get; set; }
        public string Table_Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<SalesReportBySingleTableDetail> Sales_Detail { get; set; } = new List<SalesReportBySingleTableDetail>();
    }

    public class SalesReportBySingleTableDetail
    {
        public DateTime DateTime { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public double GTotal { get { return Total - Discount; } }
    }
}
