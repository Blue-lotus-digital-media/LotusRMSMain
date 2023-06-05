using LotusRMS.Models.Dto.InventoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface IInventoryRepository : IBaseRepository<LotusRMS_Inventory>
    {
        Task UpdateAsync(LotusRMS_Inventory inventory);
        
    }
}
