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

        
        Task UpdateAsync(LotusRMS_Order Order);
        Task UpdateCompleteOrderAsync(LotusRMS_Order order);
        Task UpdateKitchenCompleteAsync(string OrderNo, Guid OrderDetailId);
        Task UpdateStatusAsync(Guid Id);
        Task CancelOrderAsync(string OrderNo, Guid OrderDetailId);
        Task CompleteOrderDetailAsync(string OrderNo, Guid OrderDetailId);
      /*  IEnumerable<LotusRMS_Order> GetFirstOrDefault(Guid TableId);*/
    }
}
