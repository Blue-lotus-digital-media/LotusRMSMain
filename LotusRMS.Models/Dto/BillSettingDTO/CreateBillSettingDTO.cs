using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.BillSettingDTO
{
    public class CreateBillSettingDTO
    {
        public string BillPrefix { get; set; }
        public string BillTitle { get; set; }
        public string BillAddress { get; set; }
        public string BillNote { get; set; }

        public bool IsPhone { get; set; } = true;
        public bool IsPanOrVat { get; set; } = true;

        public bool IsActive { get; set; }
    }
}
