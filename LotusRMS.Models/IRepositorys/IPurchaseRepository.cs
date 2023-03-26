using LotusRMS.Models.Dto.PurchaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IPurchaseRepository:IBaseRepository<LotusRMS_Purchase>
    {
        void Update(LotusRMS_Purchase obj);
        IEnumerable<LotusRMS_Purchase> GetByDateRange(DateTime startDate, DateTime endDate);

        
    }
}
