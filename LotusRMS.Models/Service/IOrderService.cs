using LotusRMS.Models.Dto.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IOrderService
    {
        Task<Guid> CreateAsync(CreateOrderDTO dto);
        
        Task<Guid> UpdateAsync(UpdateOrderDTO dto);
        Task<Guid> UpdateCompleteOrderAsync(UpdateOrderDTO dto);

        Task<Guid> CancelOrderAsync(string OrderNo, Guid OrderDetailId);
        Task<Guid> UpdateKitchenCompleteAsync(string OrderNo, Guid OrderDetailId);
        Task<Guid> CompleteOrderDetailAsync(string OrderNo, Guid OrderDetailId);
       
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<Guid> PrintKotAsync(Guid OrderId, List<LotusRMS_Order_Details> orderDetails);
        
        Task<IEnumerable<LotusRMS_Order>> GetAllActiveOrderAsync();
        Task<IEnumerable<LotusRMS_Order>> GetAllByDateRangeAsync(DateTime startDate,DateTime EndDate);
        Task<IEnumerable<LotusRMS_Order>> GetAllAsync();
        Task<LotusRMS_Order?> GetByGuidAsync(Guid Id);
        Task<LotusRMS_Order?> GetFirstOrDefaultByTableIdAsync(Guid TableId);
        Task<LotusRMS_Order?> GetFirstOrDefaultByOrderNoAsync(string orderNo);
        Task<LotusRMS_Order?> GetFirstOrDefaultByOrderIdAsync(Guid orderId);
        Task<Guid> ReleaseTableAsync(string OrderNo);
        Task<bool> AddOrderItemAsync(AddOrderItemDTO dto);   

        

    }
}
