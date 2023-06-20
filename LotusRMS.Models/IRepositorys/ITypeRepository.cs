using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ITypeRepository : IBaseRepository<LotusRMS_Product_Type>
    {
        Task UpdateAsync(LotusRMS_Product_Type type);
        Task UpdateStatusAsync(Guid Id);
    }
}
