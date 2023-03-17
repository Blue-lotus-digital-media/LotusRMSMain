using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IInvoiceRepository:IBaseRepository<LotusRMS_Invoice>
    {
        void Update(LotusRMS_Invoice obj);
        void PrintCopy(Guid id);
        int GetMaxInvoice();
    }
}
