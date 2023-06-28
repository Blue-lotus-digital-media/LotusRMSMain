
using LotusRMS.Models.Dto.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ICustomerService
    {
        Task<Guid> CreateAsync(CreateCustomerDTO dto);
        Task UpdateAsync(UpdateCustomerDTO dto);
        Task UpdateDueAsync(UpdateCustomerDTO dto);
        Task<IEnumerable<LotusRMS_Customer>> GetAllAsync(); 
        Task<LotusRMS_Customer?> GetByGuidAsync(Guid id); 
        Task<LotusRMS_Customer?> GetFirstOrDefaultByIdAsync(Guid id); 
        Task<IEnumerable<LotusRMS_Customer>> GetAllAvailableAsync();

        Task UpdateStatusAsync(Guid Id);

    }
}
