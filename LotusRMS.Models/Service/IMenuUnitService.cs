using LotusRMS.Models.Dto.MenuUnitDTO;
using LotusRMS.Models.Dto.UnitDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IMenuUnitService
    {
        Task<Guid> Create(CreateMenuUnitDTO dto);
        Guid Update(UnitUpdateDto dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Menu_Unit> GetAll();
        public LotusRMS_Menu_Unit GetByGuid(Guid Id);
        public LotusRMS_Menu_Unit GetFirstOrDefaultById(Guid Id);

    }
}
