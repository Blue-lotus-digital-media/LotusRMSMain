using LotusRMS.Models.Dto.SupplierDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface ISupplierService
    {
        Guid Create(CreateSupplierDTO dto);
        LotusRMS_Supplier GetByGuid(Guid Id);
        LotusRMS_Supplier GetFirstOrDefaultById(Guid Id);
        IEnumerable<LotusRMS_Supplier> GetAll(); 
        IEnumerable<LotusRMS_Supplier> GetAllAvailable();
        void Update(UpdateSupplierDTO dto);
        void UpdateStatus(Guid Id);
    }
}
