using LotusRMS.Models.Dto.CheckoutDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
        public async Task<Guid> CreateAsync(CreateCheckoutDTO dto)
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
           await _CheckoutRepository.AddAsync(checkout).ConfigureAwait(false);
           await _CheckoutRepository.UpdateOrderAsync(checkout.Order_Id).ConfigureAwait(false);
            var invoiceId =await _invoiceService.CreateAsync(checkout.Id).ConfigureAwait(false);
            return invoiceId;

        }

        public async Task<IEnumerable<LotusRMS_Checkout>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<LotusRMS_Checkout> GetByGuidAsync(Guid Id)
        {
           return await _CheckoutRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateCheckoutDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        private string GetInvoiceNo()
        {
            var invoiceNo = "";
            return invoiceNo;

        }

        public async Task<Guid> SetInvoiceAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LotusRMS_Checkout>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            //return _CheckoutRepository.GetQueryable().Where(x => x.DateTime >= startDate && x.DateTime <= endDate, includeProperties: "Order,Order.Order_Details,Order.Order_Details.Menu,Order.User,Order.Table");
            return await _CheckoutRepository.GetAllAsync(x => x.DateTime >= startDate && x.DateTime <= endDate, includeProperties: "Order,Order.Order_Details,Order.Order_Details.Menu,Order.User,Order.Table");
        }
    }
}
