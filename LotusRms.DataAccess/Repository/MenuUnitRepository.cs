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

      

        public void Update(LotusRMS_Menu_Unit lUnit)
        {
            var unit = _dal.LotusRMS_Menu_Units.FirstOrDefault(s => s.Id ==  lUnit.Id);
            if (unit != null)
            {
                unit.Update(lUnit.Unit_Name, lUnit.Unit_Symbol, lUnit.Unit_Description);

                Save();
            }
        }
        public void UpdateStatus(Guid Id)
        {
            var unit = _dal.LotusRMS_Menu_Units.FirstOrDefault(s => s.Id == Id);
            if (unit != null)
            {
                unit.Status = !unit.Status;

                Save();
            }
        }
    }
}
