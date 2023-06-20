using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IBillSettingRepository:IBaseRepository<LotusRMS_BillSetting>
    {
        Task UpdateAsync(LotusRMS_BillSetting obj);
        Task UpdateStatusAsync(Guid Id);

        Task UpdateActiveAsync(Guid Id);
    }
}
