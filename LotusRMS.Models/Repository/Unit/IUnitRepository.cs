using LotusRMS.Models.Dto.UnitDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Repository.Unit
{
    public interface IUnitRepository
    {
        Task Update(LotusRMS_Unit unit);
    }
}
