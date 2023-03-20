
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
        Guid Create(CreateCustomerDTO dto);
        void Update(UpdateCustomerDTO dto);
        IEnumerable<LotusRMS_Customer> GetAll(); 
        LotusRMS_Customer GetByGuid(Guid id); 
        LotusRMS_Customer GetFirstOrDefaultById(Guid id); 
        IEnumerable<LotusRMS_Customer> GetAllAvailable();

        void UpdateStatus(Guid Id);

    }
}
