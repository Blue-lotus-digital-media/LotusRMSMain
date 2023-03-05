
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IMenuRepository : IBaseRepository<LotusRMS_Menu>
    {
        void Update(LotusRMS_Menu menu);
        void UpdateStatus(Guid Id);

    }
}
