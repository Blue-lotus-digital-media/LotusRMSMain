using LotusRMS.Models.Dto.MenuDTO;
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
        public Guid Create(CreateMenuDTO dto)
        {
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
            _IMenuRepository.Add(menu);
            _IMenuRepository.Save();
            return menu.Id;

        }
       

        public async Task<Guid> CreateAsync(CreateMenuDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LotusRMS_Menu> GetAll()
        {
            return _IMenuRepository.GetAll(includeProperties: "Menu_Unit,Menu_Category,Menu_Type,Menu_Details,Menu_Details.Divison,Menu_Incredians,Menu_Incredians.Product,Menu_Incredians.Unit");
        } public IEnumerable<LotusRMS_Menu> GetAllAvailable()
        {
            return _IMenuRepository.GetAll(filter:x=>!x.IsDelete, includeProperties: "Menu_Unit,Menu_Category,Menu_Type,Menu_Details,Menu_Details.Divison,Menu_Incredians,Menu_Incredians.Product,Menu_Incredians.Unit");
        }

        public async Task<IEnumerable<LotusRMS_Menu>> GetAllAsync()
        {
            return await _IMenuRepository.GetAllAsync();
        }

        public LotusRMS_Menu GetByGuid(Guid Id)
        {
            return _IMenuRepository.GetByGuid(Id);
        }

        public async Task<LotusRMS_Menu> GetByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Menu GetFirstOrDefault(Guid Id)
        {

            return _IMenuRepository.GetFirstOrDefault(filter: x => x.Id == Id, includeProperties: "Menu_Unit,Menu_Category,Menu_Type,Menu_Details,Menu_Details.Divison,Menu_Incredians,Menu_Incredians.Product,Menu_Incredians.Unit");
        }

        public Guid Update(UpdateMenuDTO dto)
        {
            //using var tx = new TransactionScope();
            var menu = _IMenuRepository.GetFirstOrDefault(x => x.Id == dto.Id, includeProperties: "Menu_Details,Menu_Incredians");

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


            _IMenuRepository.Update(menu);
            //todo logic

            // tx.Complete();
            return menu.Id;
        }

        public async Task<Guid> UpdateAsync(UpdateMenuDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateStatus(Guid Id)
        {
            _IMenuRepository.UpdateStatus(Id);

            _IMenuRepository.Save();
            return Id;
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

    }
}
