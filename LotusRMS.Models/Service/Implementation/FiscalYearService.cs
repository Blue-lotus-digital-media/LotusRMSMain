using LotusRMS.Models.Dto.FiscalYearDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class FiscalYearService : IFiscalYearService
    {
        private readonly IFiscalYearRepository _fiscalYearRepository;

        public FiscalYearService(IFiscalYearRepository fiscalYearRepository)
        {
            _fiscalYearRepository = fiscalYearRepository;
        }

        public bool CheckActive(Guid Id)
        {
            var year = _fiscalYearRepository.GetByGuid(Id);
            if (year.IsActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<Guid> Create(CreateFiscalYearDTO dto)
        {
            var FY = new LotusRMS_FiscalYear()
            {
                Name = dto.Name,
                StartDateAD = dto.StartDateAD.ToString(),
                EndDateAD = dto.EndDateAD.ToString(),
                StartDateBS=dto.StartDateBS,
                EndDateBS=dto.EndDateBS,
                IsActive = dto.IsActive
            };
           _fiscalYearRepository.Add(FY);

            if (dto.IsActive)
            {
                var years = _fiscalYearRepository.GetFirstOrDefault(x => x.IsActive && x.Id!=FY.Id);
                if (years != null)
                {
                    years.IsActive = false;
                }

            }
            _fiscalYearRepository.Save();
            return Task.FromResult(FY.Id);



        }

        public IEnumerable<LotusRMS_FiscalYear> GetAll()
        {
            return _fiscalYearRepository.GetAll();
        }

        public IEnumerable<LotusRMS_FiscalYear> GetAllAvailable()
        {
            return _fiscalYearRepository.GetAll(x => !x.IsDelete);
        }

        public LotusRMS_FiscalYear GetByGuid(Guid Id)
        {
            return _fiscalYearRepository.GetFirstOrDefault(x => x.Id == Id);
        }
        public LotusRMS_FiscalYear GetActiveYear()
        {
            return _fiscalYearRepository.GetFirstOrDefault(x => x.IsActive);
        }

        public Guid Update(UpdateFiscalYearDTO dto)
        {
            var fiscalYear = new LotusRMS_FiscalYear();
            fiscalYear.Id = dto.Id;
            fiscalYear.StartDateAD = dto.StartDateAD;
            fiscalYear.StartDateBS = dto.StartDateBS;
            fiscalYear.EndDateAD = dto.EndDateAD;
            fiscalYear.EndDateBS = dto.EndDateBS;
            fiscalYear.IsActive = dto.IsActive;

             
           _fiscalYearRepository.Update(fiscalYear);
            return dto.Id;
            
            
        }

        public Guid UpdateActive(Guid Id)
        {
            _fiscalYearRepository.UpdateActive(Id);

            return Id;
           
        }

        public Guid UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
