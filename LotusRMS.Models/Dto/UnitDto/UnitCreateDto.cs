using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.UnitDto
{
    public class UnitCreateDto
    {
        public string UnitName { get; set; }
        public string UnitSymbol { get; set; }
        public string UnitDescription { get; set; }
        public bool Status { get; set; }
    }
}
