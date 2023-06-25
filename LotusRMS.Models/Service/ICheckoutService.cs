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
        Task<Guid> CreateAsync(CreateCheckoutDTO dto);
        Task<Guid> UpdateAsync(UpdateCheckoutDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<IEnumerable<LotusRMS_Checkout>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Checkout>> GetAllByDateRangeAsync(DateTime startDate,DateTime endDate);
        Task<LotusRMS_Checkout> GetByGuidAsync(Guid Id);

        Task<Guid> SetInvoiceAsync(Guid Id);


    }
}
