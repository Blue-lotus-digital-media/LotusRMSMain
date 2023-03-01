using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IUnitOfWork:IDisposable
    {
        IUnitRepository Unit { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        ISP_Call SP_Call { get; }
        void Save();

    }
}
