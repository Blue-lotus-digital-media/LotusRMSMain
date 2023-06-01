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

        public void Update(LotusRMS_Menu lMenu)
        {

            Save();
        }

        public void UpdateStatus(Guid Id)
        {
            var type = _dal.LotusRMS_Menus.FirstOrDefault(x => x.Id == Id);
            if (type != null)
            {
                type.Status = !type.Status;
                Save();
            }
        }
    }
}
