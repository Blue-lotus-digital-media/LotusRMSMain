using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IFiscalYearRepository :IBaseRepository<LotusRMS_FiscalYear>
    {
        public Task<LotusRMS_FiscalYear?> GetActiveAsync();
        public Task<LotusRMS_FiscalYear?> GetByMyIdAsync(Guid Id);
        public Task UpdateAsync(LotusRMS_FiscalYear obj);
        public Task UpdateStatusAsync(Guid Id);
        public Task UpdateActiveAsync(Guid Id);
       

    }
}
