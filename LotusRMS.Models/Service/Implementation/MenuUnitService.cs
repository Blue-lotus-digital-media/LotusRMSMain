using LotusRMS.Models.Dto.MenuUnitDTO;
using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LotusRMS.Models.Service.Implementation
{
    public class MenuUnitService : IMenuUnitService
    {
        private readonly IMenuUnitRepository _menuUnitRepository;

        public MenuUnitService(IMenuUnitRepository menuUnitRepository)
        {
            _menuUnitRepository = menuUnitRepository;
        }

        public async Task<Guid> CreateAsync(CreateMenuUnitDTO dto)
        {
           // using var tx = new TransactionScope();
            var unit = new LotusRMS_Menu_Unit(dto.UnitName, dto.UnitSymbol, dto.UnitDescription);
            unit.UnitDivision = new List<LotusRMS_Unit_Division>();
            foreach(var item in dto.Unit_Division)
            {
                unit.UnitDivision.Add(new LotusRMS_Unit_Division()
                {
                    Title = item.Title,
                    Value = item.Value
                });
            }
          await _menuUnitRepository.AddAsync(unit).ConfigureAwait(false);
            await _menuUnitRepository.SaveAsync().ConfigureAwait(false);
           // tx.Complete();
            return unit.Id;


        }

        public async Task<Guid> UpdateAsync(UpdateMenuUnitDTO dto)
        {
         //   using var tx = new TransactionScope();
            var unit = await _menuUnitRepository.GetByGuidAsync(dto.Id).ConfigureAwait(false) ?? throw new Exception();

            unit.Update(unit_Name: dto.UnitName, unit_Symbol: dto.UnitSymbol, unit_Description: dto.UnitDescription);

            await _menuUnitRepository.UpdateAsync(unit).ConfigureAwait(false);
            await _menuUnitRepository.SaveAsync().ConfigureAwait(false);
            //todo logic

           // tx.Complete();
            return unit.Id;
        }

        public async Task<IEnumerable<LotusRMS_Menu_Unit>> GetAllAsync()
        {
            return await _menuUnitRepository.GetAllAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<LotusRMS_Menu_Unit>> GetAllAvailableAsync()
        {
            return await _menuUnitRepository.GetAllAsync(x=>x.Status && !x.IsDelete).ConfigureAwait(false);
        } 
        public async Task<LotusRMS_Menu_Unit> GetByGuidAsync(Guid Id)
        {
            return await _menuUnitRepository.GetByGuidAsync(Id);
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {

            await _menuUnitRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
            await _menuUnitRepository.SaveAsync().ConfigureAwait(false);
            return Id;

        }

        public async Task<LotusRMS_Menu_Unit> GetFirstOrDefaultByIdAsync(Guid Id)
        {
            var unit = await _menuUnitRepository.GetFirstOrDefaultAsync(x => x.Id == Id, includeProperties: "UnitDivision").ConfigureAwait(false);
            return unit;
        }

        public async Task<bool> IsDuplicateAsync(string Name, Guid? Id)
        {
            var menuUnit = await _menuUnitRepository.GetFirstOrDefaultAsync(x => x.Unit_Name == Name);

            if (menuUnit == null)
            {
                return false;
            }
            else
            {
                if (Id != Guid.Empty && menuUnit.Id == Id)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
