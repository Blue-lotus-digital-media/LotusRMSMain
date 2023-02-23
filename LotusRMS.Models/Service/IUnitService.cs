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
        Task<Guid> Update(UnitUpdateDto dto);
    }
}
