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



        public void Update(LotusRMS_Order order)
        {
            var orders = GetFirstOrDefault(filter: x => x.Id == order.Id, includeProperties: "Order_Details,User");
            if (orders != null)
            {

                Save();
            }
        }
        public void UpdateCompleteOrder(LotusRMS_Order orders)
        {
            var order = GetFirstOrDefault(filter: x => x.Id == orders.Id, includeProperties: "Order_Details,User");
            if (order != null)
            {

                order.Order_Details.AddRange(orders.Order_Details);

                Save();
            }
        }

        public void UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void CancelOrder(string OrderNo, Guid OrderDetailId)
        {
            var order = GetFirstOrDefault(filter: x => x.Order_No == OrderNo, includeProperties: "Order_Details,User");
            if (order != null)
            {
                var orderDetail = order.Order_Details.FirstOrDefault(x => x.Id == OrderDetailId);
                if (orderDetail != null)
                {
                    order.Order_Details.Remove(orderDetail);
                }
            }
            Save();
        }

        public void CompleteOrderDetail(string OrderNo, Guid OrderDetailId)
        {
            var order = GetFirstOrDefault(filter: x => x.Order_No == OrderNo, includeProperties: "Order_Details,User");
            if (order != null)
            {
                var orderDetail = order.Order_Details.FirstOrDefault(x => x.Id == OrderDetailId);
                var orderDetail1 = order.Order_Details.FirstOrDefault(x => x.MenuId==orderDetail.MenuId && x.IsComplete);
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
            Save();
        }
    }
}
