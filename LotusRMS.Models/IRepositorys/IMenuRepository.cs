
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IMenuRepository : IBaseRepository<LotusRMS_Menu>
    {
        Task UpdateAsync(LotusRMS_Menu menu);
        Task UpdateStatusAsync(Guid Id);

    }
}
