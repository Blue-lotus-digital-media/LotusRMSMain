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


        public async Task<Guid> Create(CreateCategoryDTO dto)
        {
            var category = new LotusRMS_Product_Category(dto.Category_Name,dto.Category_Description,dto.Type_Id);
            _CategoryRepository.Add(category);
            _CategoryRepository.Save();
            return category.Id;

        }

        public IEnumerable<LotusRMS_Product_Category> GetAll()
        {
            return _CategoryRepository.GetAll(includeProperties: "Product_Type");
        }

        public LotusRMS_Product_Category GetByGuid(Guid Id)
        {
            return _CategoryRepository.GetByGuid(Id);
        }

        public Guid Update(UpdateCategoryDTO dto)
        {
            var category=_CategoryRepository.GetByGuid(dto.Id) ?? throw new Exception();
            category.Update(category_Name: dto.Category_Name, category_Description: dto.Category_Description,type_Id:dto.Type_Id);
            _CategoryRepository.Update(category);
            _CategoryRepository.Save();
            return category.Id;
        }
        public Guid UpdateStatus(Guid Id)
        {
           
            _CategoryRepository.UpdateStatus(Id);
           
            return Id;
        }

    }
}
