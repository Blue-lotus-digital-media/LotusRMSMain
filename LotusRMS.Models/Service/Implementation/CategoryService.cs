using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class CategoryService : ICategoryService
    {

        public ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }


        public async Task<Guid> CreateAsync(CreateCategoryDTO dto)
        {
            var category = new LotusRMS_Product_Category(dto.Category_Name,dto.Category_Description,dto.Type_Id);
            await _CategoryRepository.AddAsync(category).ConfigureAwait(false);
            await _CategoryRepository.SaveAsync().ConfigureAwait(false);
            return category.Id;

        }

        public async Task<IEnumerable<LotusRMS_Product_Category>> GetAllAsync()
        {
            return await _CategoryRepository.GetAllAsync(includeProperties: "Product_Type").ConfigureAwait(false);
        }   public async Task<IEnumerable<LotusRMS_Product_Category>> GetAllAvailableAsync()
        {
            return await _CategoryRepository.GetAllAsync(x=>x.Status&& !x.IsDelete, includeProperties: "Product_Type").ConfigureAwait(false);
        }

        public async Task<LotusRMS_Product_Category> GetByGuidAsync(Guid Id)
        {
            return await _CategoryRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<bool> IsDuplicateName(string Name, Guid Id = new Guid())
        {
            var category = await _CategoryRepository.GetFirstOrDefaultAsync(x => x.Category_Name == Name).ConfigureAwait(false);
            if (category == null) {
                return false;
            }
            else
            {
                if(Id!=Guid.Empty && Id == category.Id)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public async Task<Guid> UpdateAsync(UpdateCategoryDTO dto)
        {
            var category = await _CategoryRepository.GetByGuidAsync(dto.Id).ConfigureAwait(false) ?? throw new Exception();
            category.Update(category_Name: dto.Category_Name, category_Description: dto.Category_Description,type_Id:dto.Type_Id);
            await _CategoryRepository.UpdateAsync(category).ConfigureAwait(false);
          await  _CategoryRepository.SaveAsync().ConfigureAwait(false);
            return category.Id;
        }
        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
           
            await _CategoryRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
           
            return Id;
        }

    }
}
