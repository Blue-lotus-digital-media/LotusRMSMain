using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ITableRepository : IBaseRepository<LotusRMS_Table>
    {
        Task UpdateAsync(LotusRMS_Table table);
        Task UpdateStatusAsync(Guid Id);
        Task<bool> UpdateReservedAsync(Guid Id);
    
}
}
