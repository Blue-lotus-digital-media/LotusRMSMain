using LotusRMS.Models.Dto.TypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ITableTypeService
    {
        Task<Guid> CreateAsync(CreateTypeDTO dto);
        Task<Guid> UpdateAsync(UpdateTypeDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);
       Task<LotusRMS_Table_Type> GetFirstOrDefaultByIdAsync(Guid typeId);
        Task<IEnumerable<LotusRMS_Table_Type>> GetAllAvailableAsync();
        Task<IEnumerable<LotusRMS_Table_Type>> GetAllAsync();
        Task<LotusRMS_Table_Type> GetByGuidAsync(Guid Id);
        Task<bool> IsDuplicateName(string Name, Guid Id = new Guid());
    }
}
