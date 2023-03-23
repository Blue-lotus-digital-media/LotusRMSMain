using LotusRMS.Models.IRepositorys;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<RMSUser> _UserManager;

        public UserService(UserManager<RMSUser> userManager)
        {
            _UserManager = userManager;
        }

        public IEnumerable<RMSUser> GetAllUser()
        {
            return _UserManager.Users;
        }

        public IEnumerable<RMSUser> GetExceptSA()
        {
            /*var users = _UserManager.GetUsersInRoleAsync();
            return _iUserRepository.GetAll();*/

            throw new NotImplementedException();
        }

        public RMSUser GetUser(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
