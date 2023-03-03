using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class MenuCategoryService : IMenuCategoryService
    {

        public IMenuCategoryRepository _IMenuCategoryRepository;
        public MenuCategoryService(IMenuCategoryRepository iMenuCategoryRepository)
        {
            _IMenuCategoryRepository = iMenuCategoryRepository;
        }


        public async Task<Guid> Create(CreateCategoryDTO dto)
        {
            var category = new LotusRMS_Menu_Category(dto.Category_Name,dto.Category_Description,dto.Type_Id);
            _IMenuCategoryRepository.Add(category);
           
            return category.Id;

        }

        public IEnumerable<LotusRMS_Menu_Category> GetAll()
        {
            return _IMenuCategoryRepository.GetAll(includeProperties: "Product_Type");
        }

        public LotusRMS_Menu_Category GetByGuid(Guid Id)
        {
            return _IMenuCategoryRepository.GetByGuid(Id);
        }

        public Guid Update(UpdateCategoryDTO dto)
        {
            var category= _IMenuCategoryRepository.GetByGuid(dto.Id) ?? throw new Exception();
            category.Update(category_Name: dto.Category_Name, category_Description: dto.Category_Description,type_Id:dto.Type_Id);
            _IMenuCategoryRepository.Update(category);
            return category.Id;
        }
        public Guid UpdateStatus(Guid Id)
        {

            _IMenuCategoryRepository.UpdateStatus(Id);
           
            return Id;
        }

    }
}
