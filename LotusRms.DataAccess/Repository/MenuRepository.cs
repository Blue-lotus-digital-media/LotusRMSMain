using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
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
            var menu = GetFirstOrDefault(x => x.Id == lMenu.Id,includeProperties: "Menu_Details");
            if (menu != null)
            {
                menu.Update(
                    item_name: lMenu.Item_Name,
                    unit_Id: lMenu.Unit_Id,
                    type_Id: lMenu.Type_Id,
                    category_Id: lMenu.Category_Id,
                    orderTo: lMenu.OrderTo
                );
                if (lMenu.Image != null)
                {
                    menu.Image = lMenu.Image;
                }
                Save();
            }
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
