using LotusRMS.Models.Dto.CheckoutDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class CheckoutService : ICheckoutService
    {
        public ICheckoutRepository _CheckoutRepository;
        public IInvoiceService _invoiceService;
        private readonly IInventoryService _iInventoryService;
        public CheckoutService(ICheckoutRepository checkoutRepository,
            IInvoiceService invoiceService,
            IInventoryService iInventoryService)
        {
            _CheckoutRepository = checkoutRepository;
            _invoiceService = invoiceService;
            _iInventoryService = iInventoryService;
        }
        public async Task<Guid> Create(CreateCheckoutDTO dto)
        {
            if (dto.Customer_Name == null)
            {
                dto.Customer_Name = "Cash";
            }

            var checkout = new LotusRMS_Checkout()
            {
                Order_Id = dto.Order_Id,
                Customer_Id = dto.Customer_Id,
                Customer_Name = dto.Customer_Name,
                Customer_Address = dto.Customer_Address,
                Customer_Contact = dto.Customer_Contact,
                Total = dto.Total,
                Discount_Type = dto.Discount_Type,
                Discount = dto.Discount,
                DateTime = CurrentTime.DateTimeNow(),
                Payment_Mode = dto.Payment_Mode,
                Paid_Amount=dto.Paid_Amount,
                Invoice_No = GetInvoiceNo()
            };
         

            _CheckoutRepository.Add(checkout);

            _CheckoutRepository.UpdateOrder(checkout.Order_Id);


            

            var invoiceId = _invoiceService.Create(checkout.Id);





            return invoiceId;

        }

        public IEnumerable<LotusRMS_Checkout> GetAll()
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Checkout GetByGuid(Guid Id)
        {
           return _CheckoutRepository.GetByGuid(Id);
        }

        public Guid Update(UpdateCheckoutDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }

        public string GetInvoiceNo()
        {
            var invoiceNo = "";
            return invoiceNo;

        }

        public Guid SetInvoice(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LotusRMS_Checkout> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            return _CheckoutRepository.GetAll(x => x.DateTime >= startDate && x.DateTime <= endDate, includeProperties: "Order,Order.Order_Details,Order.Order_Details.Menu,Order.User,Order.Table");
        }
    }
}
