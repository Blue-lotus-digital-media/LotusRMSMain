using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICheckoutRepository:IBaseRepository<LotusRMS_Checkout>
    {
        void Update(LotusRMS_Checkout obj);
    }
}
