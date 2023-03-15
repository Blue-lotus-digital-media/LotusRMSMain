using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.BillSetting
{
    public class BillSettingVM
    {
        public Guid Id { get; set; }
        public string BillPrefix { get; set; }
        public string BillTitle { get; set; }
        public string BillAddress { get; set; }
        public string BillNote { get; set; }
        public bool IsPhone { get; set; }
        public bool IsPanOrVat { get; set; }
        public bool IsActive { get; set; }
    }
}
