using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
