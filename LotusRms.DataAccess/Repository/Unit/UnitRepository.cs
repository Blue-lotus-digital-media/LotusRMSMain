using LotusRMS.Models;
using LotusRMS.Models.Repository.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository.Unit
{
    public class UnitRepository : Repository<LotusRMS_Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _dal;
        public UnitRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public Task Update(LotusRMS_Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
