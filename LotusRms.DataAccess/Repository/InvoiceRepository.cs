using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class InvoiceRepository : BaseRepository<LotusRMS_Invoice>, IInvoiceRepository
    {
        private readonly ApplicationDbContext _dal;
        public InvoiceRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public int GetMaxInvoice()
        {
            var maxInvoice=_dal.LotusRMS_Invoices.Max(x=>(int?)x.Invoice_No)??0; 
           return maxInvoice;
        }

        public void PrintCopy(Guid id)
        {
            var invoice = GetByGuid(id);
            invoice.Print_Count ++;
            Save();
        }

        public void Update(LotusRMS_Invoice obj)
        {
            throw new NotImplementedException();
        }
    }
}
