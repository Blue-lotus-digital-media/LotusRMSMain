using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ISupplierRepository:IBaseRepository<LotusRMS_Supplier>
    {
        void Update(LotusRMS_Supplier obj);
        void UpdateStatus(Guid Id);
        
    }
}
