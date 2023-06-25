using LotusRMS.Models.Dto.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service
{
    public interface IProductService
    {
        Task<Guid> CreateAsync(CreateProductDTO dto);
        Task<Guid> UpdateAsync(UpdateProductDTO dto);
        Task<Guid> UpdateStatusAsync(Guid Id);

        Task<IEnumerable<LotusRMS_Product>> GetAllAsync();
        Task<IEnumerable<LotusRMS_Product>> GetAllAvailableAsync();
        Task<LotusRMS_Product?> GetByGuidAsync(Guid Id);



    }
}
