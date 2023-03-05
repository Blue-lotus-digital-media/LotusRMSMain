using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IMenuUnitRepository: IBaseRepository<LotusRMS_Menu_Unit>
    {
        void Update(LotusRMS_Menu_Unit unit);
        void UpdateStatus(Guid Id);
    }
}
