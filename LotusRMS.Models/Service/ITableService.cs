using LotusRMS.Models.Dto.TableDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ITableService
    {
        Task<Guid> CreateAsync(CreateTableDTO dto);
        Task<Guid> UpdateAsync(UpdateTableDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);
        Task<bool> UpdateReservedAsync(Guid Id);
        Task<bool> IsDuplicateName(string name, Guid Id = new Guid());
        Task<IEnumerable<LotusRMS_Table>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Table>> GetAllAvailableAsync();
         Task<IEnumerable<LotusRMS_Table>> GetAllReservedAsync();
         Task<LotusRMS_Table> GetByGuidAsync(Guid Id);
         Task<LotusRMS_Table> GetFirstOrDefaultByIdAsync(Guid Id);
         Task<IEnumerable<LotusRMS_Table>> GetAllByTypeIdAsync(Guid Id);
       
    }
}
