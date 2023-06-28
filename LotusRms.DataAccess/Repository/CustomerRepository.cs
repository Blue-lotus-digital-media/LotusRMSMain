using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{

    public class CustomerRepository : BaseRepository<LotusRMS_Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _dal;
        public CustomerRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_Customer obj)
        {
            var customer =await GetFirstOrDefaultAsync(filter: x => x.Id == obj.Id, includeProperties: "DueBooks").ConfigureAwait(false);
            customer.Update(name: obj.Name,address:obj.Address,contact:obj.Contact);
            customer.PanOrVat = obj.PanOrVat;
            await SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateDueAsync(LotusRMS_Customer obj)
        {
            var customer =await GetFirstOrDefaultAsync(filter: x => x.Id == obj.Id, includeProperties: "DueBooks").ConfigureAwait(false);
           foreach(var due in obj.DueBooks)
            {
                customer.DueBooks.Add(due);
            }
            await SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateStatusAsync(Guid id)
        {
            var customer = await GetByGuidAsync(id).ConfigureAwait(false);
            if (customer != null)
            {
                customer.Status = !customer.Status;
                await SaveAsync().ConfigureAwait(false);
            }
        }
    }
}
