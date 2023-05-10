using LotusRMS.Models.Dto.MenuUnitDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.MenuUnit
{
    public class UpdateMenuUnitVM:CreateMenuUnitVM
    {
        public Guid Id { get; set; }
        public ICollection<UpdateMenuUnitDivisionDTO> Unit_Division { get; set; }

        public UpdateMenuUnitVM()
        {
            Unit_Division = new List<UpdateMenuUnitDivisionDTO>();
        }
    }
}
