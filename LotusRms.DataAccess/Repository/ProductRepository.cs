using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class ProductRepository : BaseRepository<LotusRMS_Product>, IProductRepository
    {
        public readonly ApplicationDbContext _dal;
        public ProductRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }
        

        public async Task UpdateAsync(LotusRMS_Product lProduct)
        {
            var product =await GetByGuidAsync(lProduct.Id).ConfigureAwait(false);
            if (product != null)
            {
                product.Update(product_Name: lProduct.Product_Name,
                    product_Description: lProduct.Product_Description,
                    product_Unit_Id: lProduct.Product_Unit_Id,
                    product_Category_Id: lProduct.Product_Category_Id,
                    unit_Quantity: (float)lProduct.Unit_Quantity,
                    product_Type_Id:lProduct.Product_Type_Id
                   
                    );
            }
            await SaveAsync().ConfigureAwait(false);
        }
        public async Task UpdateStatusAsync(Guid Id)
        {
            var product = await GetByGuidAsync(Id).ConfigureAwait(false);
            product.Status = !product.Status;

            await SaveAsync();
        }

    }
}
