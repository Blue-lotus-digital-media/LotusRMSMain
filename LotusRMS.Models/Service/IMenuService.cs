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
        Guid Create(CreateMenuDTO dto);
        Guid Update(UpdateMenuDTO dto);
        Task<Guid> UpdateAsync(UpdateMenuDTO dto);
        Guid UpdateStatus(Guid Id);
        Task<Guid> UpdateStatusAsync(Guid Id);
        IEnumerable<LotusRMS_Menu> GetAll();
        Task<IEnumerable<LotusRMS_Menu>> GetAllAsync();
        LotusRMS_Menu GetByGuid(Guid Id);
        Task<LotusRMS_Menu> GetByGuidAsync(Guid Id);
    }
}
