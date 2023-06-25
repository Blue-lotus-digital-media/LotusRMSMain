using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICheckoutRepository:IBaseRepository<LotusRMS_Checkout>
    {
        Task Update(LotusRMS_Checkout obj);
        Task UpdateOrderAsync(Guid id);
    }
}
