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
        Guid Create(CreateProductDTO dto);
        Task<Guid> Update(UpdateProductDTO dto);
        Guid UpdateStatus(Guid Id);

        public IEnumerable<LotusRMS_Product> GetAll();
        public LotusRMS_Product GetByGuid(Guid Id);



    }
}
