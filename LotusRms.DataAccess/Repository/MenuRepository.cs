using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class MenuRepository : BaseRepository<LotusRMS_Menu>, IMenuRepository
    {
        public readonly ApplicationDbContext _dal;
        public MenuRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task UpdateAsync(LotusRMS_Menu lMenu)
        {
            await SaveAsync();
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            var type = await GetFirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            if (type != null)
            {
                type.Status = !type.Status;
                await SaveAsync();
            }
        }
    }
}
