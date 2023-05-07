using LotusRMS.Models.Dto.MenuUnitDTO.MenuDivisionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.MenuUnitDTO
{
    public class CreateMenuUnitDTO
    {
        public string UnitName { get; set; }
        public string UnitSymbol { get; set; }
        public string UnitDescription { get; set; }
        public ICollection<CreateMenuUnitDivisionDTO> Unit_Division { get; set; }


     
    }
}
