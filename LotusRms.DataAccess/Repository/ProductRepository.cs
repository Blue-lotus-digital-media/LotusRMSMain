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
        

        public void Update(LotusRMS_Product lProduct)
        {
            var product = GetByGuid(lProduct.Id);
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
        }
        public void UpdateStatus(Guid Id)
        {
            var product = GetByGuid(Id);
            product.Status = !product.Status;
            
            
        }

    }
}
