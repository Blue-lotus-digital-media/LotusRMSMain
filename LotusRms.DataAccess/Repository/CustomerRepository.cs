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

        public void Update(LotusRMS_Customer obj)
        {
            var customer = GetFirstOrDefault(filter: x => x.Id == obj.Id, includeProperties: "DueBooks");
            customer.Update(name: obj.Name,address:obj.Address,contact:obj.Contact);
            customer.PanOrVat = obj.PanOrVat;
            Save();
        }

        public void UpdateDue(LotusRMS_Customer obj)
        {
            var customer = GetFirstOrDefault(filter: x => x.Id == obj.Id, includeProperties: "DueBooks");
           foreach(var due in obj.DueBooks)
            {
                customer.DueBooks.Add(due);
            }
            Save();
        }

        public void UpdateStatus(Guid id)
        {
            var customer = GetByGuid(id);
            if (customer != null)
            {
                customer.Status = !customer.Status;
                Save();
            }
        }
    }
}
