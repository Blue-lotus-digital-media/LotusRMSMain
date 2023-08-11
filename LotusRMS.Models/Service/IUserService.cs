using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IUserService
    {
        RMSUser GetUser(Guid Id);
        IEnumerable<RMSUser> GetAllUser();
        IEnumerable<RMSUser> GetExceptSA();

        
    }
}
