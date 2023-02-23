using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.Repository.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public Task<Guid> Create(UnitCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Update(UnitUpdateDto dto)
        {
            var unit = _unitRepository.GetByIdAsync(dto.Id) ?? throw new Exception();
            unit.Update(dto.UnitDescription, );
            _unitRepository.Update(unit);
            //todo logic
            
        }
    }
}
