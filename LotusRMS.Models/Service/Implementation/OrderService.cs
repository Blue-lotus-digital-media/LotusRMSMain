using LotusRMS.Models.Dto.OrderDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace LotusRMS.Models.Service.Implementation
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _IOrderRepository;

        public OrderService(IOrderRepository _iOrderRepository)
        {
            _IOrderRepository = _iOrderRepository;
        }
        public async Task<bool> AddOrderItemAsync(AddOrderItemDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateAsync(CreateOrderDTO dto)
        {
            var order = new LotusRMS_Order()
            {
                Table_Id=dto.Table_Id,
                OrderBy=dto.UserId,
                Order_No=RandomStringGenerator.RandomString(6),
                DateTime=CurrentTime.DateTimeNow(),
                Order_Details = new List<LotusRMS_Order_Details>()
            };
            foreach(var item in dto.OrderDetails)
            {
                var orderDetail = new LotusRMS_Order_Details()
                {
                    MenuId = item.Menu_Id,
                    Quantity = (float)item.Quantity,
                    Quantity_Id=item.Quantity_Id,
                    Rate = (float)item.Rate,
                    Remarks=item.Remarks
                };
                order.Order_Details.Add(orderDetail);
            }

            await _IOrderRepository.AddAsync(order).ConfigureAwait(false);
            return order.Id;
        }
    public async Task<IEnumerable<LotusRMS_Order>> GetAllAsync()
        {
            var orders = await _IOrderRepository.GetAllAsync(x => x.IsCheckout).ConfigureAwait(false);
            return orders;
        }
        public async Task<IEnumerable<LotusRMS_Order>> GetAllActiveOrderAsync()
        {
            return await _IOrderRepository.GetAllAsync(x => !x.IsCheckout, includeProperties: "Order_Details,Order_Details.Menu,Order_Details.Menu.Menu_Details,Order_Details.Menu.Menu_Details.Divison,User,Table").ConfigureAwait(false);
           
        }
        public async Task<IEnumerable<LotusRMS_Order>> GetAllByDateRangeAsync(DateTime StartDate,DateTime EndDate)
        {
            var orders =await _IOrderRepository.FindBy(x => x.DateTime >= StartDate && x.DateTime <= EndDate, includeProperties: "Order_Details,Order_Details.Menu,Order_Details.Menu.Menu_Details,Order_Details.Menu.Menu_Details.Divison,User,Table").ConfigureAwait(false);
            return orders;
        }

        public async Task<LotusRMS_Order?> GetByGuidAsync(Guid Id)
        {
            return await _IOrderRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<LotusRMS_Order?> GetFirstOrDefaultByTableIdAsync(Guid TableId)
        {
            var orders =await _IOrderRepository.GetFirstOrDefaultAsync(filter: x=>x.Table_Id==TableId && !x.IsCheckout ,includeProperties: "Order_Details,User,Table").ConfigureAwait(false);
            return orders;
        }
       

        public async Task<LotusRMS_Order?> GetFirstOrDefaultByOrderNoAsync(string orderNo)
        {
            var orders =await _IOrderRepository.GetFirstOrDefaultAsync(filter: x=>x.Order_No==orderNo ,includeProperties: "Order_Details,Order_Details.Menu,Order_Details.Menu.Menu_Details,Order_Details.Menu.Menu_Details.Divison,Order_Details.Menu.Menu_Incredians,User,Table").ConfigureAwait(false);
            return orders;
        } 

        public async Task<LotusRMS_Order?> GetFirstOrDefaultByOrderIdAsync(Guid orderId)
        {
            var orders = await _IOrderRepository.GetFirstOrDefaultAsync(filter: x=>x.Id==orderId ,includeProperties: "Order_Details,Order_Details.Menu,Order_Details.Menu.Menu_Details,Order_Details.Menu.Menu_Details.Divison,Order_Details.Menu.Menu_Incredians,User,Table").ConfigureAwait(false);
            return orders;
        }

        public async Task<Guid> UpdateAsync(UpdateOrderDTO dto)
        {
            throw new NotImplementedException();
        }
        public async Task<Guid> UpdateCompleteOrderAsync(UpdateOrderDTO dto)
        {
            var order = new LotusRMS_Order()
            {
                Id=dto.Order_Id,
                Order_Details=new List<LotusRMS_Order_Details>()
            };

            foreach(var item in dto.OrderDetail)
            {
                var orderDetail = new LotusRMS_Order_Details()
                {
                    MenuId = item.Menu_Id,
                    Quantity = (float)item.Quantity,
                    Quantity_Id=item.Quantity_Id,
                    Rate = (float)item.Rate,
                    Remarks = item.Remarks
                };
                order.Order_Details.Add(orderDetail);
            }
           await _IOrderRepository.UpdateCompleteOrderAsync(order).ConfigureAwait(false);
            return order.Id;
        }

    


        public Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CancelOrderAsync(string OrderNo, Guid OrderDetailId)
        {
            await _IOrderRepository.CancelOrderAsync(OrderNo, OrderDetailId).ConfigureAwait(false);
            return OrderDetailId; 
        } 
        public async Task<Guid> UpdateKitchenCompleteAsync(string OrderNo, Guid OrderDetailId)
        {
            await _IOrderRepository.UpdateKitchenCompleteAsync(OrderNo, OrderDetailId).ConfigureAwait(false);
            return OrderDetailId; 
        }
        public async Task<Guid> CompleteOrderDetailAsync(string OrderNo, Guid OrderDetailId)
        {
            await _IOrderRepository.CompleteOrderDetailAsync(OrderNo, OrderDetailId).ConfigureAwait(false);
            return OrderDetailId; 
        }

        public async Task<Guid> PrintKotAsync(Guid OrderId,List<LotusRMS_Order_Details> orderDetails )
        {
            var order =await _IOrderRepository.GetFirstOrDefaultAsync(x => x.Id == OrderId, includeProperties: "Order_Details,User,Table").ConfigureAwait(false);
            foreach(var item in orderDetails)
            {
                order.Order_Details.Where(x => x.Id == item.Id).First().IsPrinted = true;
            }
            await _IOrderRepository.UpdateAsync(order).ConfigureAwait(false);
            return OrderId;
        }

        public async Task<IEnumerable<LotusRMS_Order_Details>> GetUnPrintedDetailAsync(string orderNo)
        {
            var order =await _IOrderRepository.GetFirstOrDefaultAsync(x => x.Order_No == orderNo, includeProperties: "Order_Details,User,Table").ConfigureAwait(false);
            var orderDetail = new List<LotusRMS_Order_Details>();
            foreach(var item in order.Order_Details) {
                if (!item.IsPrinted)
                {
                    orderDetail.Add(item);
                }
            }
            return orderDetail;
        }

        public async Task<Guid> ReleaseTableAsync(string OrderNo)
        {
            var order =await GetFirstOrDefaultByOrderNoAsync(orderNo: OrderNo).ConfigureAwait(false);
            var tableId = order.Table_Id;
            await _IOrderRepository.RemoveAsync(order).ConfigureAwait(false);
            return tableId;

        }
    }
}
