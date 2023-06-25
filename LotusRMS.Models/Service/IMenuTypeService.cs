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
        Task<Guid> UpdateAsync(UpdateTypeDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);
        Task<IEnumerable<LotusRMS_Menu_Type>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Menu_Type>> GetAllAvailableAsync();
        Task<LotusRMS_Menu_Type?> GetByGuidAsync(Guid Id);

    }
}
