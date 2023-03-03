using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class MenuCategoryRepository : BaseRepository<LotusRMS_Menu_Category>, IMenuCategoryRepository
    {
        private readonly ApplicationDbContext _dal;
        public MenuCategoryRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

      

        public void Update(LotusRMS_Menu_Category obj)
        {
            var category = _dal.LotusRMS_Menu_Categories.FirstOrDefault(x => x.Id == obj.Id);
            if (category != null)
            {
                category.Update(category_Name: obj.Category_Name, category_Description: obj.Category_Description,obj.Type_Id);
                Save();

            }
        }

        public void UpdateStatus(Guid Id)
        {
            var type = _dal.LotusRMS_Menu_Categories.FirstOrDefault(x => x.Id == Id);
            if (type != null)
            {
                type.Status = !type.Status;
                Save();
            }
        }
    }
}
