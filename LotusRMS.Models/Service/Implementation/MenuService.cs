using LotusRMS.Models.Dto.MenuDTO;
using LotusRMS.Models.Helper;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class MenuService : IMenuService
    {
        public readonly IMenuRepository _IMenuRepository;
        public MenuService(IMenuRepository iMenuRepository)
        {
            _IMenuRepository = iMenuRepository;
        }
            public async Task<Guid> CreateAsync(CreateMenuDTO dto)
            {
            using var scope = TransactionScopeHelper.GetInstance;
                var  menu= new LotusRMS_Menu();
            menu.Update(
                item_name: dto.Item_name,
                unit_Id: dto.Unit_Id,
                type_Id: dto.Type_Id,
                category_Id:dto.Category_Id,
                orderTo: dto.OrderTo
            );
            menu.Menu_Details = new List<LotusRMS_MenuDetail>();
            foreach(var item in dto.Menu_Details)
            {
                var detail = new LotusRMS_MenuDetail()
                {
                    Quantity=item.Quantity,
                    Rate=item.Rate,
                    Default=item.Default
                };
                menu.Menu_Details.Add(detail);
            }
            foreach(var item in dto.Menu_Incredians)
            {
                var incredian = new LotusRMS_MenuIncredians()
                {
                    Product_Id = item.Product_Id,
                    Quantity = item.Quantity,
                    Unit_Id = item.Unit_Id

                };
                menu.Menu_Incredians.Add(incredian);
            }
            if (dto.Image != null)
            {
                menu.Image = dto.Image;
            }
            await _IMenuRepository.AddAsync(menu).ConfigureAwait(false);
            await _IMenuRepository.SaveAsync().ConfigureAwait(false);
            scope.Complete();
            return menu.Id;

        }
       
        public async Task<IEnumerable<LotusRMS_Menu>> GetAllAsync()
        {
            return await _IMenuRepository.GetAllAsync(includeProperties: "Menu_Unit,Menu_Category,Menu_Type,Menu_Details,Menu_Details.Divison,Menu_Incredians,Menu_Incredians.Product,Menu_Incredians.Unit").ConfigureAwait(false);
        }
        public async Task<IEnumerable<LotusRMS_Menu>> GetAllAvailableAsync()
        {
            return await _IMenuRepository.GetAllAsync(filter:x=>!x.IsDelete&&x.Status, includeProperties: "Menu_Unit,Menu_Category,Menu_Type,Menu_Details,Menu_Details.Divison,Menu_Incredians,Menu_Incredians.Product,Menu_Incredians.Unit").ConfigureAwait(false);
        }

        public async Task<LotusRMS_Menu?> GetByGuidAsync(Guid Id)
        {
            return await _IMenuRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<LotusRMS_Menu?> GetFirstOrDefaultByIdAsync(Guid Id)
        {

            return await _IMenuRepository.GetFirstOrDefaultAsync(filter: x => x.Id == Id,
                includeProperties: "Menu_Unit,Menu_Category,Menu_Type,Menu_Details,Menu_Details.Divison,Menu_Incredians,Menu_Incredians.Product,Menu_Incredians.Unit").ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateMenuDTO dto)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var menu = await _IMenuRepository.GetFirstOrDefaultAsync(x => x.Id == dto.Id, includeProperties: "Menu_Details,Menu_Incredians").ConfigureAwait(false);

            menu.Update(
                  item_name: dto.Item_name,
                unit_Id: dto.Unit_Id,
                type_Id: dto.Type_Id,
                category_Id: dto.Category_Id,
                orderTo: dto.OrderTo
                );
            foreach (var item in dto.UpdateMenuDetail)
            {
                if (item.Id != Guid.Empty)
                {
                    var menuDetail = menu.Menu_Details.Where(x => x.Id == item.Id).FirstOrDefault();
                    menuDetail.Id = item.Id;
                    menuDetail.Default = item.Default;
                        menuDetail.Quantity = item.Quantity;
                    menuDetail.Rate = item.Rate;
                  

                }
                else
                {
                    var detail = new LotusRMS_MenuDetail()
                    {

                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        Default = item.Default
                    };

                    menu.Menu_Details.Add(detail);
                }
            }
            foreach (var item in dto.UpdateMenuIncredian)
            {
                if (item.Id != Guid.Empty)
                {
                    var incredianMenu=menu.Menu_Incredians.Where(x => x.Id == item.Id).FirstOrDefault();
                    incredianMenu.Id = item.Id;
                    incredianMenu.Quantity = item.Quantity;
                    incredianMenu.Unit_Id = item.Unit_Id;
                    incredianMenu.Product_Id = item.Product_Id;
                }
                else
                {
                    var incredian = new LotusRMS_MenuIncredians()
                    {
                        Product_Id = item.Product_Id,
                        Quantity = item.Quantity,
                        Unit_Id = item.Unit_Id

                    };
                    menu.Menu_Incredians.Add(incredian);
                }
                
              /*  if (item.Id != Guid.Empty)
                {
                    incredian.Id = item.Id;
                }
                menu.Menu_Incredians.Add(incredian);*/
            }
            if (dto.Image != null)
            {
                menu.Image = dto.Image;
            }


           await _IMenuRepository.UpdateAsync(menu).ConfigureAwait(false);
            //todo logic

            scope.Complete();
            return menu.Id;
        }

       

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            await _IMenuRepository.UpdateStatusAsync(Id);
            scope.Complete();
            return Id;
        }


    }
}
