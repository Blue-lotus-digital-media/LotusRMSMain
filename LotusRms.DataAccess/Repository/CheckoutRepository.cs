using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class CheckoutRepository : BaseRepository<LotusRMS_Checkout>, ICheckoutRepository
    {
        private readonly ApplicationDbContext _dal;
        public CheckoutRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }
        public void Update(LotusRMS_Checkout obj)
        {

        }
        public void UpdateOrder(Guid Id)
        {
            var order = _dal.LotusRMS_Orders.FirstOrDefault(x => x.Id == Id);
            order.IsCheckout = true;

        }
         
    }
    }
