using LotusRMS.Models.Dto.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ICategoryService
    {
        Task<Guid> CreateAsync(CreateCategoryDTO dto);
        Task<Guid> UpdateAsync(UpdateCategoryDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);
        Task<IEnumerable<LotusRMS_Product_Category>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Product_Category>> GetAllAvailableAsync();
        Task<LotusRMS_Product_Category> GetByGuidAsync(Guid Id);
        Task<bool> IsDuplicateName(string Name, Guid Id = new Guid());


    }
}
