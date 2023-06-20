using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class CategoryRepository : BaseRepository<LotusRMS_Product_Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dal;
        public CategoryRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

      

        public async Task UpdateAsync(LotusRMS_Product_Category obj)
        {
            var category =await GetFirstOrDefaultAsync(x => x.Id == obj.Id).ConfigureAwait(false);
            if (category != null)
            {
                category.Update(category_Name: obj.Category_Name, category_Description: obj.Category_Description,obj.Type_Id);
                await SaveAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            var type = await GetFirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Status = !type.Status;
                await SaveAsync().ConfigureAwait(false);
            }
        }
    }
}
