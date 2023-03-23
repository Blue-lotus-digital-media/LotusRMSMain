using LotusRMS.Models.Dto.SupplierDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _iSupplierRepository;
        public SupplierService(ISupplierRepository iSupplierRepository)
        {
            _iSupplierRepository = iSupplierRepository;
        }
        public Guid Create(CreateSupplierDTO dto)
        {
            var supplier = new LotusRMS_Supplier()
            {
                PanOrVat=dto.PanOrVat,
                Contact1=dto.Contact1
            };
            supplier.Update(dto.FullName, dto.Address, dto.Contact);
            _iSupplierRepository.Add(supplier);
            _iSupplierRepository.Save();
            return supplier.Id;

        }

        public IEnumerable<LotusRMS_Supplier> GetAll()
        {
            return _iSupplierRepository.GetAll(x => !x.IsDelete);
        }

        public IEnumerable<LotusRMS_Supplier> GetAllAvailable()
        {
            return _iSupplierRepository.GetAll(x=>!x.IsDelete && x.Status);
        }

        public LotusRMS_Supplier GetByGuid(Guid Id)
        {
            return _iSupplierRepository.GetByGuid(Id);
        }

        public LotusRMS_Supplier GetFirstOrDefaultById(Guid Id)
        {

            return _iSupplierRepository.GetFirstOrDefault(x => x.Id==Id);
        }

        public void Update(UpdateSupplierDTO dto)
        {
            var supplier = _iSupplierRepository.GetByGuid(dto.Id);
            supplier.Update(dto.FullName, dto.Address, dto.Contact);
            supplier.PanOrVat = dto.PanOrVat;
            supplier.Contact1 = dto.Contact1;
            _iSupplierRepository.Update(supplier);

            

        }

        public void UpdateStatus(Guid Id)
        {
            _iSupplierRepository.UpdateStatus(Id);
        }
    }
}
