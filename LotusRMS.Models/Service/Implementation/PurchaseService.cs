using LotusRMS.Models.Dto.InventoryDTO;
using LotusRMS.Models.Dto.PurchaseDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IInventoryService _iInventoryService;
        public PurchaseService(IPurchaseRepository purchaseRepository, IInventoryService iInventoryService)
        {
            _purchaseRepository = purchaseRepository;
            _iInventoryService = iInventoryService;
        }

        public async Task<Guid> CreateAsync(CreatePurchaseDTO dto)
        {
            var purchase = new LotusRMS_Purchase()
            {
                Date = CurrentTime.DateTimeNow(),
                PurchaseDate = DateTime.Parse(dto.Purchase_Date),
                Bill_Amount = dto.Bill_Amount,
                Bill_No = dto.Bill_No,
                Discount = dto.Discount,
                Discount_Type = dto.Discount_Type,
                Paid_Amount = dto.Paid_Amount,
                Due=dto.Due_Amount,
                Payment_Mode = dto.Payment_Mode,
                Supplier_Id = dto.Supplier_Id,
               PurchaseDetails = new List<LotusRMS_PurchaseDetail>()

            };
            foreach(var item in dto.PurchaseDetails)
            {
                var pd = new LotusRMS_PurchaseDetail()
                {
                    Product_Id = item.Product_Id,
                    Quantity = item.Product_Quantity,
                    Rate = item.Product_Rate
                };
                purchase.PurchaseDetails.Add(pd);
            }
            _purchaseRepository.Add(purchase);
            _purchaseRepository.Save();

            foreach(var item in purchase.PurchaseDetails)
            {
                var inventory = await _iInventoryService.GetInventoryByProductIdAsync(item.Product_Id);
                if (inventory != null)
                {
                    var quantity = inventory.StockQuantity + item.Quantity;
                    var invDto = new UpdateInventoryDTO()
                    {
                        Id = inventory.Id,
                        StockQuantity = quantity,
                    };
                    await _iInventoryService.UpdateOnPurchaseAsync(invDto);
                }
                else {
                    var inv = new CreateInventoryDTO()
                    {
                        ProductId = item.Product_Id,
                        StockQuantity = item.Quantity,
                        ReorderLevel = 0

                    };
                    await _iInventoryService.CreateAsync(inv);
                
                }
            }

            return purchase.Id;
        }
        

        public IEnumerable<LotusRMS_Purchase> GetAll()
        {
            return _purchaseRepository.GetAll(includeProperties: "Supplier,PurchaseDetails,PurchaseDetails.Product,PurchaseDetails.Product.Product_Unit");

        }

        public IEnumerable<LotusRMS_Purchase> GetByDateRange(string startDate,string endDate)
        {
            var sd = DateTime.Parse(startDate);
            var ed = DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1);
            var purchase = _purchaseRepository.GetByDateRange(sd, ed).ToList();

            return purchase;
        }

        public LotusRMS_Purchase GetByGuid(Guid Id)
        {
            throw new NotImplementedException();
        }  public LotusRMS_Purchase GetFirstOrDefaultByGuid(Guid Id)
        {
            return _purchaseRepository.GetFirstOrDefault(x=>x.Id==Id, includeProperties: "Supplier,PurchaseDetails,PurchaseDetails.Product,PurchaseDetails.Product.Product_Unit");

        }

        public Guid Update(UpdatePurchaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateStatus(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
