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
                rate: dto.Rate,
                unit_Id: dto.Unit_Id,
                unit_Quantity: dto.Unit_Quantity,
                type_Id: dto.Type_Id,
                category_Id:dto.Category_Id,
                orderTo: dto.OrderTo
            
                
                );
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
            return _IMenuRepository.GetAll(includeProperties: "Menu_Unit,Menu_Category,Menu_Type");
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

        public Guid Update(UpdateMenuDTO dto)
        {
            //using var tx = new TransactionScope();
            var  menu= _IMenuRepository.GetByGuid(dto.Id) ?? throw new Exception();
            menu.Update(
                  item_name: dto.Item_name,
                rate: dto.Rate,
                unit_Id: dto.Unit_Id,
                unit_Quantity: dto.Unit_Quantity,
                type_Id: dto.Type_Id,
                category_Id: dto.Category_Id,
                orderTo: dto.OrderTo
                );
            if (dto.Image != null)
            {
                menu.Image = dto.Image;
            }

            _IMenuRepository.Update(menu);
            _IMenuRepository.Save();
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
