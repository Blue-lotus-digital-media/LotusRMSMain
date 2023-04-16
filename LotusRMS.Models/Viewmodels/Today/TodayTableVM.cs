using LotusRMS.Models.Viewmodels.Table;
using LotusRMS.Models.Viewmodels.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Today
{
    public class TodayTableVM
    {
        public TypeVM Type { get; set; }
        public List<TodayDetailVM> Table { get; set; }
    }
}
