using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore;
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
        public async Task Update(LotusRMS_Checkout obj)
        {

        }
        public async Task UpdateOrderAsync(Guid Id)
        {
            var order = await _dal.LotusRMS_Orders.FirstOrDefaultAsync(x => x.Id == Id);
            order.IsCheckout = true;
            await SaveAsync();

        }
         
    }
    }
