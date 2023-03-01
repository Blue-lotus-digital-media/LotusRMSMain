using LotusRMS.Models.Dto.ProductDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{

    public class ProductService : IProductService
    {

        public readonly IProductRepository _IProductRepository;

        public ProductService(IProductRepository iProductRepository)
        {
            _IProductRepository = iProductRepository;
        }

        public Guid Create(CreateProductDTO dto)
        {
            var product = new LotusRMS_Product(product_Name:dto.Product_Name,product_Description:dto.Product_Description,product_Unit_Id:dto.Product_Unit_Id,product_Category_Id:dto.Product_Category_Id);
            _IProductRepository.Add(product);
            _IProductRepository.Save();
            return product.Id;
        }

        public IEnumerable<LotusRMS_Product> GetAll()
        {
            return _IProductRepository.GetAll(includeProperties: "Product_Unit,Product_Category");    
        }

        public LotusRMS_Product GetByGuid(Guid Id)
        {
            return _IProductRepository.GetByGuid(Id);
        }

        public async Task<Guid> Update(UpdateProductDTO dto)
        {
            var product = _IProductRepository.GetByGuid(dto.Id) ?? throw new Exception();
            product.Update(dto.Product_Name, dto.Product_Description, dto.Product_Unit_Id, dto.Product_Category_Id);
            _IProductRepository.Update(product);

            _IProductRepository.Save();
            return product.Id;

        }

        public Guid UpdateStatus(Guid Id)
        {
            var product = _IProductRepository.GetByGuid(Id) ?? throw new Exception();
            _IProductRepository.UpdateStatus(Id);

            _IProductRepository.Save();
            return product.Id;
        }
    }
}
