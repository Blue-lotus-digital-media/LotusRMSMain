using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IProductRepository : IBaseRepository<LotusRMS_Product>
    {
        Task UpdateAsync(LotusRMS_Product product);
        Task UpdateStatusAsync(Guid Id);

    }
}
