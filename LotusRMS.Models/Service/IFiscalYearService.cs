using LotusRMS.Models.Dto.FiscalYearDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IFiscalYearService
    {
        Task<Guid> CreateAsync(CreateFiscalYearDTO dto);
       Task<Guid> UpdateAsync(UpdateFiscalYearDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<IEnumerable<LotusRMS_FiscalYear>> GetAllAsync();
        Task<IEnumerable<LotusRMS_FiscalYear>> GetAllAvailableAsync();
        Task<LotusRMS_FiscalYear> GetByGuidAsync(Guid Id);
        Task<LotusRMS_FiscalYear> GetActiveYearAsync();

        Task<Guid> UpdateActiveAsync(Guid Id);
        Task<bool> CheckActiveAsync(Guid Id);

      Task<bool> IsDuplicateAsync(string FiscalYear,Guid? Id);

    }
}
