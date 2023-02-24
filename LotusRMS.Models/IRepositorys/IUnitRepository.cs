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
        void Update(LotusRMS_Unit unit);
        void Save();
    }

}
