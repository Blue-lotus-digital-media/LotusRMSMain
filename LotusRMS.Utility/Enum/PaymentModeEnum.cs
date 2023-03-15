using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Utility.Enum
{
    public enum PaymentModeEnum
    {
        [Display(Name ="Cash")]
        Cash=1,
        [Display(Name = "Credit")]
        Credit=2,
        [Display(Name = "Bank")]
        Bank=3,
        [Display(Name = "Esewa")]
        Esewa=4,
        [Display(Name = "Khalti")]
        Khalti=5
    }
}
