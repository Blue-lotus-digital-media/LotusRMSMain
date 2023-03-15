using LotusRMS.Models.Dto.BillSettingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IBillSettingService
    {
        Task<Guid> Create(CreateBillSettingDTO dto);
        Guid Update(UpdateBillSettingDTO dto);
        Guid UpdateActive(Guid Id);

        IEnumerable<LotusRMS_BillSetting> GetAll();
        IEnumerable<LotusRMS_BillSetting> GetAllAvailable();
        LotusRMS_BillSetting GetByGuid(Guid Id);
        LotusRMS_BillSetting GetActive();
        bool CheckActive();

    }
}
