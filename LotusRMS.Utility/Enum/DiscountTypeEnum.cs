using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LotusRMS.Utility.Enum
{
    public enum DiscountTypeEnum
    {
        [Display(Name = "Rs.")]
        Cash=1,
        [Display(Name = "%")]
        Percentage=2

    }
}
