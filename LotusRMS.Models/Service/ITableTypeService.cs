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
        Guid Create(CreateTypeDTO dto);
        Guid Update(UpdateTypeDTO dto);
        Task<Guid> UpdateAsync(UpdateTypeDTO dto);
        Guid UpdateStatus(Guid Id);
        Task<Guid> UpdateStatusAsync(Guid Id);
        LotusRMS_Table_Type GetFirstOrDefaultById(Guid typeId);

        IEnumerable<LotusRMS_Table_Type> GetAll();
        IEnumerable<LotusRMS_Table_Type> GetAllAvailable();
        Task<IEnumerable<LotusRMS_Table_Type>> GetAllAsync();
        LotusRMS_Table_Type GetByGuid(Guid Id);
        Task<LotusRMS_Table_Type> GetByGuidAsync(Guid Id);
    }
}
