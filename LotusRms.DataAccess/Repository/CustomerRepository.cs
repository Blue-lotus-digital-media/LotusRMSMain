using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
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

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
