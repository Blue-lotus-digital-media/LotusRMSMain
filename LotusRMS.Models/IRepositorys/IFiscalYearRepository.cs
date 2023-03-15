using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IFiscalYearRepository :IBaseRepository<LotusRMS_FiscalYear>
    {
        public void Update(LotusRMS_FiscalYear obj);
        public void UpdateStatus(Guid Id);
        public void UpdateActive(Guid Id);

    }
}
