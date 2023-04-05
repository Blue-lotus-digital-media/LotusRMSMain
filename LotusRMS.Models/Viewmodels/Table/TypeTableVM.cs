using LotusRMS.Models.Viewmodels.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Table
{
    public class TypeTableVM
    {
        public Guid Selected { get; set; }
        public TypeVM Type { get; set; }
        public List<TableVM> Table { get; set; }

    }
}
