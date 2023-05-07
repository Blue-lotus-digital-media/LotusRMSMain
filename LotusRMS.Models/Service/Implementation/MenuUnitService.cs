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

        public async Task<Guid> Create(CreateMenuUnitDTO dto)
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
            _menuUnitRepository.Add(unit);
            _menuUnitRepository.Save();
           // tx.Complete();
            return unit.Id;


        }

        public Guid Update(UnitUpdateDto dto)
        {
         //   using var tx = new TransactionScope();
            var unit = _menuUnitRepository.GetByGuid(dto.Id) ?? throw new Exception();

            unit.Update(unit_Name: dto.UnitName, unit_Symbol: dto.UnitSymbol, unit_Description: dto.UnitDescription);

            _menuUnitRepository.Update(unit);
            _menuUnitRepository.Save();
            //todo logic

           // tx.Complete();
            return unit.Id;
        }

        public IEnumerable<LotusRMS_Menu_Unit> GetAll()
        {
            return _menuUnitRepository.GetAll();
        } 
        public LotusRMS_Menu_Unit GetByGuid(Guid Id)
        {
            return _menuUnitRepository.GetByGuid(Id);
        }

        public Guid UpdateStatus(Guid Id)
        {

            _menuUnitRepository.UpdateStatus(Id);
            _menuUnitRepository.Save();
            return Id;

        }

        public LotusRMS_Menu_Unit GetFirstOrDefaultById(Guid Id)
        {
            var unit = _menuUnitRepository.GetFirstOrDefault(x => x.Id == Id, includeProperties: "UnitDivision");
            return unit;
        }
    }
}
