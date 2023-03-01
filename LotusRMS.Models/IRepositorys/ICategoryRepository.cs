using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICategoryRepository : IBaseRepository<LotusRMS_Product_Category>
    {
        void Update(LotusRMS_Product_Category obj);
        void Save();
    }
}
