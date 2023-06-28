using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICustomerRepository:IBaseRepository<LotusRMS_Customer>
    {
        Task UpdateAsync(LotusRMS_Customer obj);
        Task UpdateDueAsync(LotusRMS_Customer obj);
        Task UpdateStatusAsync(Guid id);

    }
}
