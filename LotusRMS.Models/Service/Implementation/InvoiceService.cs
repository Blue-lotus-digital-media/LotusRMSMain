using LotusRMS.Models.Dto.InvoiceDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IBillSettingService _billSettingService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IFiscalYearService _iFiscalYearService;

        public InvoiceService(IInvoiceRepository invoiceRepository, IBillSettingService billSettingService, IFiscalYearService iFiscalYearService)
        {
            _invoiceRepository = invoiceRepository;
            _billSettingService = billSettingService;
            _iFiscalYearService = iFiscalYearService;
        }

        public Guid Create(Guid id)
        {
            var bill = _billSettingService.GetActive();

            var billString = bill.BillPrefix;
           
            
            var fiscalyear = _iFiscalYearService.GetActiveYear();
            if (bill.IsFiscalYear)
            {
                billString = billString + "" + fiscalyear.Name;
            }
            var invoiceNo = _invoiceRepository.GetMaxInvoice() + 1;
            billString = billString + "_" +invoiceNo ;

            var model = new LotusRMS_Invoice()
            {
                BillSetting_Id=bill.Id,
                FiscalYear_Id=fiscalyear.Id,
                Invoice_String=billString,
                Invoice_No=invoiceNo,
                Checkout_Id=id,

            };
            _invoiceRepository.Add(model);
            _invoiceRepository.Save();
            return model.Id;

            
        }
       

        public IEnumerable<LotusRMS_Invoice> GetAll()
        {
            return _invoiceRepository.GetAll(includeProperties: "Checkout,FiscalYear,BillSetting,Order");
        }

        public LotusRMS_Invoice GetByGuid(Guid Id)
        {
            return _invoiceRepository.GetByGuid(Id);
        }

        public LotusRMS_Invoice GetFirstOrDefault(Guid Id)
        {
           return _invoiceRepository.GetFirstOrDefault(includeProperties: "Checkout,FiscalYear,BillSetting,Checkout.Order,Checkout.Order.User,Checkout.Order.Table,Checkout.Order.Table.Table_Type,Checkout.Order.Order_Details,,Checkout.Order.Order_Details.Menu,,Checkout.Order.Order_Details.Menu.Menu_Unit");
        }

        public void PrintCopy(Guid Id)
        {
            _invoiceRepository.PrintCopy(Id);
        }
    }
}
