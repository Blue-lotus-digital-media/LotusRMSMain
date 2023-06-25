using LotusRMS.Models.Dto.UnitDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IUnitRepository:IBaseRepository<LotusRMS_Unit>
    {
        Task UpdateAsync(LotusRMS_Unit unit);
        Task UpdateStatusAsync(Guid Id);
    }

}
