using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IProductRepository : IBaseRepository<LotusRMS_Product>
    {
        void Update(LotusRMS_Product product);
        void UpdateStatus(Guid Id);
        void Save();

    }
}
