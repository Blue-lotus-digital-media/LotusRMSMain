using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IBillSettingRepository:IBaseRepository<LotusRMS_BillSetting>
    {
        void Update(LotusRMS_BillSetting obj);
        void UpdateStatus(Guid Id);

        public void UpdateActive(Guid Id);
    }
}
