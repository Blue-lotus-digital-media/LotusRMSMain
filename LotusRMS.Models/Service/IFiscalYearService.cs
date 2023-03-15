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
        Task<Guid> Create(CreateFiscalYearDTO dto);
        Guid Update(UpdateFiscalYearDTO dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_FiscalYear> GetAll();
        public IEnumerable<LotusRMS_FiscalYear> GetAllAvailable();
        public LotusRMS_FiscalYear GetByGuid(Guid Id);
        public LotusRMS_FiscalYear GetActiveYear();

        Guid UpdateActive(Guid Id);
        bool CheckActive(Guid Id);

    }
}
