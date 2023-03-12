using LotusRMS.Models.Dto.OrderDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DateTime=CurrentTime.DateTimeNow().ToLongDateString(),
                Order_Details = new List<LotusRMS_Order_Details>()
            };
            foreach(var item in dto.OrderDetails)
            {
                var orderDetail = new LotusRMS_Order_Details()
                {
                    MenuId = item.Menu_Id,
                    Quantity = item.Quantity,
                    Rate = item.Rate
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LotusRMS_Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Order GetByGuid(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<LotusRMS_Order> GetByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Order GetFirstOrDefaultByTableId(Guid TableId)
        {
            var orders = _IOrderRepository.GetFirstOrDefault(filter: x=>x.Table_Id==TableId ,includeProperties: "Order_Details,User");
            return orders;
        }
        public LotusRMS_Order GetFirstOrDefaultByOrderNo(string orderNo)
        {
            var orders = _IOrderRepository.GetFirstOrDefault(filter: x=>x.Order_No==orderNo ,includeProperties: "Order_Details , User");
            return orders;
        }

        public Guid Update(UpdateOrderDTO dto)
        {
            throw new NotImplementedException();
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
    }
}
