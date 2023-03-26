using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.Dto.PurchaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IPurchaseService
    {
        Guid Create(CreatePurchaseDTO dto);
        Guid Update(UpdatePurchaseDTO dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Purchase> GetAll();
        public IEnumerable<LotusRMS_Purchase> GetByDateRange(string startDate,string endDate);
        public LotusRMS_Purchase GetByGuid(Guid Id);
        public LotusRMS_Purchase GetFirstOrDefaultByGuid(Guid Id);
    }
}
