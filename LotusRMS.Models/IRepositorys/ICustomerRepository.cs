using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICustomerRepository:IBaseRepository<LotusRMS_Customer>
    {
        void Update();
        void UpdateStatus(Guid id);

    }
}
