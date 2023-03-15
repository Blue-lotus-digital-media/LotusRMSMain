using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class FiscalYearRepository : BaseRepository<LotusRMS_FiscalYear>, IFiscalYearRepository
    {
        private readonly ApplicationDbContext _dal;
        public FiscalYearRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public void Update(LotusRMS_FiscalYear obj)
        {
            var fiscalYear = GetByGuid(obj.Id);
            fiscalYear.StartDateAD = obj.StartDateAD;
            fiscalYear.StartDateBS = obj.StartDateBS;
            fiscalYear.EndDateAD = obj.EndDateAD;
            fiscalYear.EndDateBS = obj.EndDateBS;


            var activeYear = GetFirstOrDefault(x => x.IsActive);
            if (activeYear.Id != obj.Id && obj.IsActive)
            {
                activeYear.IsActive = false;
                fiscalYear.IsActive = true;
            }
            Save();
        }

        public void UpdateActive(Guid Id)
        {
            var activeYear = GetFirstOrDefault(x => x.IsActive);
            if (activeYear != null)
            {
                activeYear.IsActive = false;
            }
            var currentYear = GetByGuid(Id);
            currentYear.IsActive = true;
            Save();
        }

        public void UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
