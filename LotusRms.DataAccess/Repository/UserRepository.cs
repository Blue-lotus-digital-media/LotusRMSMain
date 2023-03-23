using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class UserRepository : BaseRepository<RMSUser>, IUserRepository
    {
        private readonly ApplicationDbContext _dal;
        
        public UserRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
      
    }
}
