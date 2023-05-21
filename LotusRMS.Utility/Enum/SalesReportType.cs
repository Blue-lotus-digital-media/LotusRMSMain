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
    public static class SalesReportType
    {
        public enum ReportType
        {
            [Display(Name = "Item wise")]
            item = 0,
            [Display(Name = "Table wise")]
            table = 1,
            [Display(Name = "Customer Wise")]
            customer = 2,
            [Display(Name = "User Wise")]
            user = 3
        }
       /* public static string GetDisplayName<T>(this T enumValue) where T : IComparable, IFormattable, IConvertible
        {
            DisplayAttribute displayAttribute = enumValue.GetType()
                                                     .GetMember(enumValue.ToString())
                                                     .First()
                                                     .GetCustomAttribute<DisplayAttribute>();

            string displayName = displayAttribute?.GetName();

            return displayName ?? enumValue.ToString();
        }*/
    }
}
