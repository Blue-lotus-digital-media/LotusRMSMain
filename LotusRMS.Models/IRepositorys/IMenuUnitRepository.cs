using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IMenuUnitRepository: IBaseRepository<LotusRMS_Menu_Unit>
    {
        Task UpdateAsync(LotusRMS_Menu_Unit unit);
        Task UpdateStatusAsync(Guid Id);
       Task<LotusRMS_Menu_Unit> GetByUnitNameAsync(string name);
    }
}
