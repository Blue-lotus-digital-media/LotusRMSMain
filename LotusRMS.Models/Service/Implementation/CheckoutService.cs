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
        public CheckoutService(ICheckoutRepository checkoutRepository)
        {
            _CheckoutRepository = checkoutRepository;
        }
        public async Task<Guid> Create(CreateCheckoutDTO dto)
        {
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
                DateTime = CurrentTime.DateTimeNow().ToString(),
                Payment_Mode = dto.Payment_Mode,
                Invoice_No = GetInvoiceNo()

            };
            _CheckoutRepository.Add(checkout);
            _CheckoutRepository.Save();
            return checkout.Id;

        }

        public IEnumerable<LotusRMS_Checkout> GetAll()
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Checkout GetByGuid(Guid Id)
        {
            throw new NotImplementedException();
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
    }
}
