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
        Guid Create(CreateOrderDTO dto);
        Guid Update(UpdateOrderDTO dto);
        Guid UpdateCompleteOrder(UpdateOrderDTO dto);

        Guid CancelOrder(string OrderNo, Guid OrderDetailId);
        Guid UpdateKitchenComplete(string OrderNo, Guid OrderDetailId);
        Guid CompleteOrderDetail(string OrderNo, Guid OrderDetailId);
        Task<Guid> UpdateAsync(UpdateOrderDTO dto);
        Guid UpdateStatus(Guid Id);
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<Guid> PrintKotAsync(Guid OrderId, List<LotusRMS_Order_Details> orderDetails);
        /*Task<LotusRMS_Order> GetUnPrintedDetail(string OrderNo);*/
        IEnumerable<LotusRMS_Order> GetAll();
        IEnumerable<LotusRMS_Order> GetAllByDateRange(DateTime startDate,DateTime EndDate);
        Task<IEnumerable<LotusRMS_Order>> GetAllAsync();
        LotusRMS_Order GetByGuid(Guid Id);
        LotusRMS_Order GetFirstOrDefaultByTableId(Guid TableId);
        LotusRMS_Order GetFirstOrDefaultByOrderNo(string orderNo);
        LotusRMS_Order GetFirstOrDefaultByOrderId(Guid orderId);
        Task<LotusRMS_Order> GetByGuidAsync(Guid Id);
        Guid ReleaseTable(string OrderNo);
        Task<bool> AddOrderItem(AddOrderItemDTO dto);   

        

    }
}
