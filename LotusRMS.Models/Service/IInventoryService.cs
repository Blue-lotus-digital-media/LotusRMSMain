using LotusRMS.Models.Dto.InventoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IInventoryService
    {
        Task<List<LotusRMS_Inventory>> GetAllInventoryAsync();
        Task UpdateAsync(UpdateInventoryDTO dto);
        Task UpdateOnPurchaseAsync(UpdateInventoryDTO dto);
        Task UpdateOnSaleAsync(UpdateInventoryDTO dto);
        Task<Guid> CreateAsync(CreateInventoryDTO dto);
        Task<LotusRMS_Inventory> GetInventoryByProductIdAsync(Guid ProductId);
        Task<LotusRMS_Inventory> GetByGuidAsync(Guid Id);



    }
}
