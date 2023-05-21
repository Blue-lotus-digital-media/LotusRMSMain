using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICustomerRepository:IBaseRepository<LotusRMS_Customer>
    {
        void Update(LotusRMS_Customer obj);
        void UpdateDue(LotusRMS_Customer obj);
        void UpdateStatus(Guid id);

    }
}
