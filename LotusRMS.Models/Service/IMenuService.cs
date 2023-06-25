using LotusRMS.Models.Dto.MenuDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IMenuService
    {
        Task<Guid> CreateAsync(CreateMenuDTO dto);
        Task<Guid> UpdateAsync(UpdateMenuDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);
        Task<IEnumerable<LotusRMS_Menu>> GetAllAvailableAsync();
        Task<IEnumerable<LotusRMS_Menu>> GetAllAsync();
        Task<LotusRMS_Menu?> GetByGuidAsync(Guid Id);
       Task<LotusRMS_Menu?> GetFirstOrDefaultByIdAsync(Guid Id);
    }
}
