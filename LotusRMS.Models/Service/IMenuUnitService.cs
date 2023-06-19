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
        Task<Guid> CreateAsync(CreateMenuUnitDTO dto);
        Task<Guid> UpdateAsync(UpdateMenuUnitDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<IEnumerable<LotusRMS_Menu_Unit>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Menu_Unit>> GetAllAvailableAsync();
        Task<LotusRMS_Menu_Unit> GetByGuidAsync(Guid Id);
        Task<LotusRMS_Menu_Unit> GetFirstOrDefaultByIdAsync(Guid Id);

        Task<bool> IsDuplicateAsync(string Name, Guid? Id);

    }
}
