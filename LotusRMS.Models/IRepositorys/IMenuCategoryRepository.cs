using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IMenuCategoryRepository : IBaseRepository<LotusRMS_Menu_Category>
    {
        void Update(LotusRMS_Menu_Category obj);
        void UpdateStatus(Guid Id);
       
    }
}
