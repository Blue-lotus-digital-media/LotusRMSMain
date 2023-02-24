using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _dal;

        public UnitOfWork(ApplicationDbContext dal)
        {
            _dal = dal;
            Unit = new UnitRepository(_dal);
            SP_Call = new SP_Call(_dal);

        }
        public IUnitRepository Unit { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _dal.Dispose();
        }
        public void Save()
        {
            _dal.SaveChanges();
        }
    }
}
