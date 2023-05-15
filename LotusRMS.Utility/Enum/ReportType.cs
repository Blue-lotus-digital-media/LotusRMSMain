using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LotusRMS.Utility.Enum
{
    public static class Enums {
        public enum ReportType
        {
            [Display(Name = "Today")]
            Today = 0,
            [Display(Name = "Last 7 Days")]
            Week = 1,
            [Display(Name = "This Month")]
            Month = 2,
            [Display(Name = "This Year")]
            Year = 3
        }
              public static string GetDisplayName<T>(this T enumValue) where T : IComparable, IFormattable, IConvertible
    {
        DisplayAttribute displayAttribute = enumValue.GetType()
                                                 .GetMember(enumValue.ToString())
                                                 .First()
                                                 .GetCustomAttribute<DisplayAttribute>();

        string displayName = displayAttribute?.GetName();

        return displayName ?? enumValue.ToString();
    }
}

}
