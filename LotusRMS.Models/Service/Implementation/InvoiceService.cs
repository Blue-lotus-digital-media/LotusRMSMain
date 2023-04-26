using LotusRMS.Models.Dto.InvoiceDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
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
        private readonly ICustomerService _iCustomerService;
        private readonly Lazy<ICheckoutService> _iCheckOutService;
        private readonly ICustomerRepository _iCustomerRepository;


        public InvoiceService(IInvoiceRepository invoiceRepository, IBillSettingService billSettingService, IFiscalYearService iFiscalYearService, ICustomerService iCustomerService,Lazy<ICheckoutService> iCheckOutService, ICustomerRepository iCustomerRepository)
        {
            _invoiceRepository = invoiceRepository;
            _billSettingService = billSettingService;
            _iFiscalYearService = iFiscalYearService;
            _iCustomerService = iCustomerService;
            _iCheckOutService = iCheckOutService;
            _iCustomerRepository = iCustomerRepository;
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

            var checkout = _iCheckOutService.Value.GetByGuid(id);
            if(checkout.Payment_Mode.ToString()=="Credit" && checkout.Customer_Id != Guid.Empty)
            {
                var customer = _iCustomerService.GetFirstOrDefaultById((Guid)checkout.Customer_Id);
                double discount = 0;
                if (checkout.Discount_Type.ToString() == "Cash")
                {
                    discount = checkout.Discount;
                }
                else
                {
                    discount = checkout.Discount / 100 * checkout.Total;
                }
                var DueAmount = checkout.Total - checkout.Paid_Amount - discount;
                var InvoiceAmount = checkout.Total - discount;
                double balanceDue = 0;
                if (customer.DueBooks.Count()!=0)
                {
                    balanceDue = customer.DueBooks.LastOrDefault().BalanceDue;

                }
                var cus=new LotusRMS_Customer()
                {
                    Id=customer.Id,
                    Name=customer.Name,
                    Address=customer.Address,
                    Contact=customer.Contact,
                    PanOrVat=customer.PanOrVat,
              DueBooks=  new List<LotusRMS_DueBook>()

                {new LotusRMS_DueBook(){
                    DueAmount = DueAmount,
                    BalanceDue= balanceDue+DueAmount,
                    Invoice_Id=model.Id,
                    PaidAmount=checkout.Paid_Amount,
                    DueDate=CurrentTime.DateTimeToday(),
                    Invoice_Amount=InvoiceAmount
                } }

                };
                _iCustomerRepository.Update(cus);


            }




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
           return _invoiceRepository.GetFirstOrDefault(x=>x.Id==Id,includeProperties: "Checkout,FiscalYear,BillSetting,Checkout.Order,Checkout.Order.User,Checkout.Order.Table,Checkout.Order.Table.Table_Type,Checkout.Order.Order_Details,,Checkout.Order.Order_Details.Menu,,Checkout.Order.Order_Details.Menu.Menu_Unit");
        }

        public void PrintCopy(Guid Id)
        {
            _invoiceRepository.PrintCopy(Id);
        }
    }
}
