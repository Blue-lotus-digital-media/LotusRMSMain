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

    public class MenuUnitRepository : BaseRepository<LotusRMS_Menu_Unit>, IMenuUnitRepository
    {
        private readonly ApplicationDbContext _dal;
        public MenuUnitRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public async Task<LotusRMS_Menu_Unit> GetByUnitNameAsync(string name)
        {
            var unit =await GetFirstOrDefaultAsync(filter: x => x.Unit_Name == name, includeProperties: "UnitDivision").ConfigureAwait(false);
            return unit;
        }

        public async Task UpdateAsync(LotusRMS_Menu_Unit lUnit)
        {
            var unit =await GetFirstOrDefaultAsync(s => s.Id ==  lUnit.Id,includeProperties: "UnitDivision").ConfigureAwait(false);
            if (unit != null)
            {
                unit.Update(lUnit.Unit_Name, lUnit.Unit_Symbol, lUnit.Unit_Description);

                await SaveAsync().ConfigureAwait(false);
            }
        }
        public async Task UpdateStatusAsync(Guid Id)
        {
            var unit = await GetByGuidAsync(Id).ConfigureAwait(false);
            if (unit != null)
            {
                unit.Status = !unit.Status;

                await SaveAsync().ConfigureAwait(false);
            }
        }
    }
}
