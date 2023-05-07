using LotusRMS.Models.Dto.MenuUnitDTO.MenuDivisionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.MenuUnitDTO
{
    public class UpdateMenuUnitDTO:CreateMenuUnitDTO
    {
        public Guid Id { get; set; }
        public ICollection<UpdateMenuUnitDivisionDTO> Unit_Division { get; set; }
    }
    public class UpdateMenuUnitDivisionDTO:CreateMenuUnitDivisionDTO
    {
        public Guid Id { get; set; }

    }
}
