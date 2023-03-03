using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ITypeRepository : IBaseRepository<LotusRMS_Product_Type>
    {
        void Update(LotusRMS_Product_Type type);
        void UpdateStatus(Guid Id);
    }
}
