using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
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
            var orders = _dal.LotusRMS_Orders.FirstOrDefault(x => x.Id == order.Id);
            if (orders != null)
            {
               
                Save();
            }
        }

        public void UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
