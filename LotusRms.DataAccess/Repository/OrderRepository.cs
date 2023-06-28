using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class OrderRepository : BaseRepository<LotusRMS_Order>, IOrderRepository
    {
        public readonly ApplicationDbContext _dal;
        public OrderRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }



        public async Task UpdateAsync(LotusRMS_Order order)
        { 
            await SaveAsync().ConfigureAwait(false);
        }   
        
        public async Task UpdateCompleteOrderAsync(LotusRMS_Order orders)
        {
            var order =await GetFirstOrDefaultAsync(filter: x => x.Id == orders.Id, includeProperties: "Order_Details,User").ConfigureAwait(false);
            if (order != null)
            {
                order.Order_Details.AddRange(orders.Order_Details);
                await SaveAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task CancelOrderAsync(string OrderNo, Guid OrderDetailId)
        {
            var order = await GetFirstOrDefaultAsync(filter: x => x.Order_No == OrderNo, includeProperties: "Order_Details,User").ConfigureAwait(false);
            if (order != null)
            {
                var orderDetail = order.Order_Details.FirstOrDefault(x => x.Id == OrderDetailId);
                if (orderDetail != null)
                {
                    order.Order_Details.Remove(orderDetail);
                }
            }
            await SaveAsync().ConfigureAwait(false);
        }

        public async Task CompleteOrderDetailAsync(string OrderNo, Guid OrderDetailId)
        {
            var order = await GetFirstOrDefaultAsync(filter: x => x.Order_No == OrderNo, includeProperties: "Order_Details,User").ConfigureAwait(false);
            if (order != null)
            {
                var orderDetail = order.Order_Details.FirstOrDefault(x => x.Id == OrderDetailId);
                var orderDetail1 = order.Order_Details.FirstOrDefault(x => x.MenuId==orderDetail.MenuId && x.IsComplete && x.Quantity_Id==orderDetail.Quantity_Id);
                if (orderDetail1 != null)
                {
                    var index = order.Order_Details.IndexOf(orderDetail1);
                    orderDetail1.Quantity += orderDetail.Quantity;
                    order.Order_Details.Remove(orderDetail);
                }
                else { 
                    var index = order.Order_Details.IndexOf(orderDetail);
                    orderDetail.IsComplete = true;
                    orderDetail.IsKitchenComplete = true;
                    order.Order_Details[index] = orderDetail;
                }
            }
            await SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateKitchenCompleteAsync(string OrderNo, Guid OrderDetailId)
        {
            var order =await GetFirstOrDefaultAsync(filter: x => x.Order_No == OrderNo, includeProperties: "Order_Details,User").ConfigureAwait(false);
            if (order != null)
            {
                var orderDetail = order.Order_Details.FirstOrDefault(x => x.Id == OrderDetailId);
                var index=order.Order_Details.IndexOf(orderDetail);
                order.Order_Details[index].IsKitchenComplete = true;
            }
            await SaveAsync().ConfigureAwait(false);
        }
    }
}
