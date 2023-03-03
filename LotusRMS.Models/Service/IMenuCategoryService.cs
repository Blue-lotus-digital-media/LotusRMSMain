using LotusRMS.Models.Dto.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IMenuCategoryService
    {
        Task<Guid> Create(CreateCategoryDTO dto);
        Guid Update(UpdateCategoryDTO dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Menu_Category> GetAll();
        public LotusRMS_Menu_Category GetByGuid(Guid Id);


    }
}
