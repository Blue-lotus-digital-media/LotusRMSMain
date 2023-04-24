using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.MenuUnit
{
    public class MenuUnitFromJson
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public List<UnitDivision> Division { get; set; }
    }

    public class UnitDivision
    {
        public string Title { get; set; }
        public double Value { get; set; }
    } 
}
