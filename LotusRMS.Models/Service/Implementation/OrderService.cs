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
        public Task<bool> AddOrderItem(AddOrderItemDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid Create(CreateOrderDTO dto)
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

            _IOrderRepository.Add(order);
            return order.Id;
        }

        public Task<Guid> CreateAsync(CreateOrderDTO dto)
        {
            throw new NotImplementedException();
        }




        public IEnumerable<LotusRMS_Order> GetAll()
        {
            var orders = _IOrderRepository.GetAll(x => x.IsCheckout);
            return orders;
        }
        public IEnumerable<LotusRMS_Order> GetAllActiveOrder()
        {
            return _IOrderRepository.GetAll(x => !x.IsCheckout, includeProperties: "Order_Details,Order_Details.Menu,Order_Details.Menu.Menu_Details,Order_Details.Menu.Menu_Details.Divison,User,Table");
           
        }
        public IEnumerable<LotusRMS_Order> GetAllByDateRange(DateTime StartDate,DateTime EndDate)
        {
            var orders = _IOrderRepository.GetAll(x => x.DateTime >= StartDate && x.DateTime <= EndDate, includeProperties: "Order_Details,Order_Details.Menu,Order_Details.Menu.Menu_Details,Order_Details.Menu.Menu_Details.Divison,User,Table");
            return orders;
        }

        public Task<IEnumerable<LotusRMS_Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Order GetByGuid(Guid Id)
        {
            return _IOrderRepository.GetByGuid(Id);
        }

        public Task<LotusRMS_Order> GetByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Order GetFirstOrDefaultByTableId(Guid TableId)
        {
            var orders = _IOrderRepository.GetFirstOrDefault(filter: x=>x.Table_Id==TableId && !x.IsCheckout ,includeProperties: "Order_Details,User,Table");
            return orders;
        }
       

        public LotusRMS_Order GetFirstOrDefaultByOrderNo(string orderNo)
        {
            var orders = _IOrderRepository.GetFirstOrDefault(filter: x=>x.Order_No==orderNo ,includeProperties: "Order_Details,User,Table");
            return orders;
        } 

        public LotusRMS_Order GetFirstOrDefaultByOrderId(Guid orderId)
        {
            var orders = _IOrderRepository.GetFirstOrDefault(filter: x=>x.Id==orderId ,includeProperties: "Order_Details,User,Table");
            return orders;
        }

        public Guid Update(UpdateOrderDTO dto)
        {
            throw new NotImplementedException();
        }
        public Guid UpdateCompleteOrder(UpdateOrderDTO dto)
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
            _IOrderRepository.UpdateCompleteOrder(order);
            return order.Id;
        }

        public Task<Guid> UpdateAsync(UpdateOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Guid CancelOrder(string OrderNo, Guid OrderDetailId)
        {
            _IOrderRepository.CancelOrder(OrderNo, OrderDetailId);
            return OrderDetailId; 
        } 
        public Guid UpdateKitchenComplete(string OrderNo, Guid OrderDetailId)
        {
            _IOrderRepository.UpdateKitchenComplete(OrderNo, OrderDetailId);
            return OrderDetailId; 
        }
        public Guid CompleteOrderDetail(string OrderNo, Guid OrderDetailId)
        {
            _IOrderRepository.CompleteOrderDetail(OrderNo, OrderDetailId);
            return OrderDetailId; 
        }

        public async Task<Guid> PrintKotAsync(Guid OrderId,List<LotusRMS_Order_Details> orderDetails )
        {
            var order =_IOrderRepository.GetFirstOrDefault(x => x.Id == OrderId, includeProperties: "Order_Details,User,Table");
            foreach(var item in orderDetails)
            {
                order.Order_Details.Where(x => x.Id == item.Id).First().IsPrinted = true;
            }
            _IOrderRepository.Update(order);
            return OrderId;
        }

        public async Task<IEnumerable<LotusRMS_Order_Details>> GetUnPrintedDetail(string orderNo)
        {
            var order =await _IOrderRepository.GetFirstOrDefaultAsync(x => x.Order_No == orderNo, includeProperties: "Order_Details,User,Table");
            var orderDetail = new List<LotusRMS_Order_Details>();
            foreach(var item in order.Order_Details) {
                if (!item.IsPrinted)
                {
                    orderDetail.Add(item);
                }
            }
            return orderDetail;
        }

        public Guid ReleaseTable(string OrderNo)
        {
            var order = GetFirstOrDefaultByOrderNo(orderNo: OrderNo);
            var tableId = order.Table_Id;
            _IOrderRepository.Remove(order);
            return tableId;

        }
    }
}
