using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    internal class SupplierRepository:BaseRepository<LotusRMS_Supplier>,ISupplierRepository
    {
        private readonly ApplicationDbContext _dal;
        public SupplierRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal; 
        }

        public void Update(LotusRMS_Supplier obj)
        {
            var supplier = GetByGuid(obj.Id);
            supplier.Update(fullName: obj.FullName, address: obj.Address, contact: obj.Contact);
            supplier.PanOrVat = obj.PanOrVat;
            supplier.Contact1 = obj.Contact1;

            Save();
        }

        public void UpdateStatus(Guid Id)
        {
            var supplier = GetByGuid(Id);
            supplier.Status = !supplier.Status;
            Save();

        }
    }
}
