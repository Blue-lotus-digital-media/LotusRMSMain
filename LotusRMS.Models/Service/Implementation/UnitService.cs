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
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<Guid> Create(UnitCreateDto dto)
        {
           // using var tx = new TransactionScope();
            var unit = new LotusRMS_Unit(dto.UnitName, dto.UnitSymbol, dto.UnitDescription);
            _unitRepository.Add(unit);
            _unitRepository.Save();
           // tx.Complete();
            return unit.Id;


        }

        public Guid Update(UnitUpdateDto dto)
        {
            using var tx = new TransactionScope();
            var unit = _unitRepository.GetByGuid(dto.Id) ?? throw new Exception();

            unit.Update(unit_Name: dto.UnitName, unit_Symbol: dto.UnitSymbol, unit_Description: dto.UnitDescription);
           
            _unitRepository.Update(unit);
            _unitRepository.Save();
            //todo logic

            tx.Complete();
            return unit.Id;
        }

        public IEnumerable<LotusRMS_Unit> GetAll()
        {
            return _unitRepository.GetAll();
        } 
        public LotusRMS_Unit GetByGuid(Guid Id)
        {
            return _unitRepository.GetByGuid(Id);
        }

        public Guid UpdateStatus(Guid Id)
        {
            
            _unitRepository.UpdateStatus(Id);
            _unitRepository.Save();
            return Id;

        }
    }
}
