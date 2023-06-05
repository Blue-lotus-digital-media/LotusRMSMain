using LotusRMS.Models;
using LotusRMS.Models.Dto.InventoryDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class InventoryRepository : BaseRepository<LotusRMS_Inventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _dal;
        public InventoryRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_Inventory inventory)
        {
            Save();
        }
    }
}
