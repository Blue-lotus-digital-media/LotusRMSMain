using LotusRMS.Models.Dto.UnitDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IUnitService
    {
        Task<Guid> Create(UnitCreateDto dto);
        Guid Update(UnitUpdateDto dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Unit> GetAll();
        public LotusRMS_Unit GetByGuid(Guid Id);

    }
}
