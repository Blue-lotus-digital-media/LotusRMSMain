using LotusRMS.Models.Dto.TypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ITypeService
    {
        Task<Guid> CreateAsync(CreateTypeDTO dto);
        
        
        Task<Guid> UpdateAsync(UpdateTypeDTO dto);
        
        Task<Guid> UpdateStatusAsync(Guid Id);
         
        Task<IEnumerable<LotusRMS_Product_Type>> GetAllAvailableAsync();
        Task<IEnumerable<LotusRMS_Product_Type>> GetAllAsync();
        
        Task<LotusRMS_Product_Type> GetByGuidAsync(Guid Id);
        Task<bool> IsDuplicateName(string Name, Guid Id = new Guid());
        
    }
}
