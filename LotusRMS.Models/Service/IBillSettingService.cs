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
        Task<Guid> CreateAsync(CreateBillSettingDTO dto);
        Task<Guid> UpdateAsync(UpdateBillSettingDTO dto);
        Task<Guid> UpdateActiveAsync(Guid Id);

        Task<IEnumerable<LotusRMS_BillSetting>> GetAllAsync();
        Task<IEnumerable<LotusRMS_BillSetting>> GetAllAvailableAsync();
       Task<LotusRMS_BillSetting> GetByGuidAsync(Guid Id);
        Task<LotusRMS_BillSetting> GetActiveAsync();
        Task<bool> CheckActiveAsync();
        Task<bool> IsDuplicateName(string Name,Guid Id=new Guid());

    }
}
