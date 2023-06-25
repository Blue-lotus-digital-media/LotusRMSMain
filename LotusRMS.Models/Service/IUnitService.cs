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
        Task<Guid> CreateAsync(UnitCreateDto dto);
        Task<Guid> UpdateAsync(UnitUpdateDto dto);
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<IEnumerable<LotusRMS_Unit>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Unit>> GetAllAvailableAsync();
        Task<LotusRMS_Unit?> GetByGuidAsync(Guid Id);

    }
}
