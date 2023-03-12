using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IOrderRepository : IBaseRepository<LotusRMS_Order>
    {

        void Update(LotusRMS_Order Order);
        void UpdateStatus(Guid Id);
      /*  IEnumerable<LotusRMS_Order> GetFirstOrDefault(Guid TableId);*/
    }
}
