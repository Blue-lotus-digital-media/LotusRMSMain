using LotusRMS.Models;
using LotusRMS.Models.Dto.PurchaseDTO;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class PurchaseRepository : BaseRepository<LotusRMS_Purchase>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _dal;
        public PurchaseRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public IEnumerable<LotusRMS_Purchase> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var purchase = GetAll(x =>x.Date >= startDate && x.Date <= endDate, includeProperties: "Supplier,PurchaseDetails,PurchaseDetails.Product,PurchaseDetails.Product.Product_Unit");
            return purchase;
        }

        public void Update(LotusRMS_Purchase obj)
        {
            var purchase = GetFirstOrDefault(x => x.Id == obj.Id, includeProperties: "Supplier,PurchaseDetails,PurchaseDetails.Product,PurchaseDetails.Product.Product_Unit");
            Save();
        }
    }
}
