using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.Dto.CheckoutDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ICheckoutService
    {
        Task<Guid> Create(CreateCheckoutDTO dto);
        Guid Update(UpdateCheckoutDTO dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Checkout> GetAll();
        public IEnumerable<LotusRMS_Checkout> GetAllByDateRange(DateTime startDate,DateTime endDate);
        public LotusRMS_Checkout GetByGuid(Guid Id);

        Guid SetInvoice(Guid Id);


    }
}
