using LotusRMS.Models.Dto.ProductDTO;
using LotusRMS.Models.Helper;
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

        public async Task<Guid> CreateAsync(CreateProductDTO dto)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var product = new LotusRMS_Product(
                 product_Name: dto.Product_Name,
                product_Description: dto.Product_Description,
                product_Unit_Id: dto.Product_Unit_Id,
                product_Category_Id: dto.Product_Category_Id,
                unit_Quantity: dto.Unit_Quantity,
                product_Type_Id: dto.Product_Type_Id
                );
            await _IProductRepository.AddAsync(product).ConfigureAwait(false);
            await _IProductRepository.SaveAsync().ConfigureAwait(false);
            scope.Complete();

            return product.Id;
        }

        public async Task<IEnumerable<LotusRMS_Product>> GetAllAsync()
        {
            return await _IProductRepository.GetAllAsync(x => !x.IsDelete,includeProperties: "Product_Unit,Product_Category,Product_Type").ConfigureAwait(false);    
        } 
        public async Task<IEnumerable<LotusRMS_Product>> GetAllAvailableAsync()
        {
            return await _IProductRepository.GetAllAsync(x=>!x.IsDelete&&x.Status, includeProperties: "Product_Unit,Product_Category,Product_Type").ConfigureAwait(false);    
        }

        public async Task<LotusRMS_Product?> GetByGuidAsync(Guid Id)
        {
            return await _IProductRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateProductDTO dto)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            var product = await _IProductRepository.GetByGuidAsync(dto.Id).ConfigureAwait(false) ?? throw new Exception();
            product.Update(
                product_Name: dto.Product_Name,
                product_Description: dto.Product_Description,
                product_Unit_Id: dto.Product_Unit_Id,
                product_Category_Id: dto.Product_Category_Id,
                unit_Quantity:dto.Unit_Quantity,
                product_Type_Id:dto.Product_Type_Id
                );
            await _IProductRepository.UpdateAsync(product);
            scope.Complete();
            return product.Id;

        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            using var scope = TransactionScopeHelper.GetInstance;
            await _IProductRepository.UpdateStatusAsync(Id).ConfigureAwait(false);
            scope.Complete();
            return Id;
        }
    }
}
