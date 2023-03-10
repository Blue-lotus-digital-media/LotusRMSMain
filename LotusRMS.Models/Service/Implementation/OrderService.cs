using LotusRMS.Models.Dto.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class OrderService : IOrderService
    {
        public Task<bool> AddOrderItem(AddOrderItemDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid Create(CreateOrderDTO dto)
        {
            throw new NotImplementedException();
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
