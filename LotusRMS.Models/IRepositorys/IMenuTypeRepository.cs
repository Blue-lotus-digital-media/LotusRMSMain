using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IMenuTypeRepository : IBaseRepository<LotusRMS_Menu_Type>
    {
        Task UpdateAsync(LotusRMS_Menu_Type type);
        Task UpdateStatusAsync(Guid Id);

    }
}
