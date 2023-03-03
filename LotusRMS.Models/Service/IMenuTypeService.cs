using LotusRMS.Models.Dto.TypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IMenuTypeService
    {
        Task<Guid> CreateAsync(CreateTypeDTO dto);
        Guid Create(CreateTypeDTO dto);
        Guid Update(UpdateTypeDTO dto);
        Task<Guid> UpdateAsync(UpdateTypeDTO dto);
        Guid UpdateStatus(Guid Id);
        Task<Guid> UpdateStatusAsync(Guid Id);
        IEnumerable<LotusRMS_Menu_Type> GetAll();
        Task<IEnumerable<LotusRMS_Menu_Type>> GetAllAsync();
        LotusRMS_Menu_Type GetByGuid(Guid Id);
        Task<LotusRMS_Menu_Type> GetByGuidAsync(Guid Id);
    }
}
