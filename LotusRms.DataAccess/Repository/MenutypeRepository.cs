using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class MenuTypeRepository : BaseRepository<LotusRMS_Menu_Type>, IMenuTypeRepository
    {
        public readonly ApplicationDbContext _dal;
        public MenuTypeRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public void Update(LotusRMS_Menu_Type lType)
        {
            var type = _dal.LotusRMS_Menu_Types.FirstOrDefault(x => x.Id == lType.Id);
            if (type != null)
            {
                type.Update(type.Type_Name, type.Type_Description);
                Save();
            }
        }

        public void UpdateStatus(Guid Id)
        {
            var type = _dal.LotusRMS_Menu_Types.FirstOrDefault(x => x.Id == Id);
            if (type != null)
            {
                type.Status = !type.Status;
                Save();
            }
        }
    }
}
