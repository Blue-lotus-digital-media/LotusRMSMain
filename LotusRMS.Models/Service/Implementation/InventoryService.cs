using LotusRMS.Models.Dto.InventoryDTO;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _iInventoryRepository;

        public InventoryService(IInventoryRepository iInventoryRepository)
        {
            _iInventoryRepository = iInventoryRepository;
        }

        public async Task<Guid> CreateAsync(CreateInventoryDTO dto)
        {
            var inventory = new LotusRMS_Inventory()
            {
                StockQuantity = dto.StockQuantity,
                Product_Id = dto.ProductId,
                ReorderLevel = dto.ReorderLevel
            };
            _iInventoryRepository.Add(inventory);
            await _iInventoryRepository.SaveAsync();
            return inventory.Id;
        }

        public async Task<LotusRMS_Inventory> GetInventoryByProductIdAsync(Guid ProductId)
        {
            var inventory = await _iInventoryRepository.GetFirstOrDefaultAsync(filter: x => x.Product_Id == ProductId);
            return inventory;
        } 
        public async Task<LotusRMS_Inventory> GetByGuidAsync(Guid Id)
        {
            var inventory = await _iInventoryRepository.GetByGuidAsync(Id);
            return inventory;
        }

        public async Task UpdateAsync(UpdateInventoryDTO dto)
        {
            var inv = await _iInventoryRepository.GetByGuidAsync(dto.Id);

            inv.StockQuantity = dto.StockQuantity;
            inv.ReorderLevel = dto.ReorderLevel;

            await _iInventoryRepository.UpdateAsync(inv);
        }

        public async Task UpdateOnPurchaseAsync(UpdateInventoryDTO dto)
        {
            var inv = await _iInventoryRepository.GetByGuidAsync(dto.Id);
            inv.StockQuantity = dto.StockQuantity;
            
            if (!inv.IsPurchased)
            {
                inv.IsPurchased = true;
            }

            await _iInventoryRepository.UpdateAsync(inv);
        }

        public async Task UpdateOnSaleAsync(UpdateInventoryDTO dto)
        {
            var inv = await _iInventoryRepository.GetByGuidAsync(dto.Id);
            inv.StockQuantity = dto.StockQuantity;

            await _iInventoryRepository.UpdateAsync(inv);
        }

        public async Task<List<LotusRMS_Inventory>> GetAllInventoryAsync()
        {
            var inv = await _iInventoryRepository.GetAllAsync(includeProperties: "Product,Product.Product_Unit,Product.Product_Category,Product.Product_Type");
            return inv.ToList();
        }
    }
}
