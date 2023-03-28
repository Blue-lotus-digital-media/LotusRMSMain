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
        Task UpdateAsync(LotusRMS_Order Order);
        void UpdateCompleteOrder(LotusRMS_Order order);
        void UpdateStatus(Guid Id);
        void CancelOrder(string OrderNo, Guid OrderDetailId);
        void CompleteOrderDetail(string OrderNo, Guid OrderDetailId);
      /*  IEnumerable<LotusRMS_Order> GetFirstOrDefault(Guid TableId);*/
    }
}
