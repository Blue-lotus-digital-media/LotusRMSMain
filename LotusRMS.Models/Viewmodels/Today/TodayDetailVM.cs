using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Today
{
    public class TodayDetailVM
    {
        public Guid Table_Id { get; set; }
        public string Table_Name{ get; set; }
        public bool IsReserved { get; set; }
        public double Transaction { get; set; }
    }
}
