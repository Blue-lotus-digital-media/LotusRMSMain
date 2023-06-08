using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin,Cashier")]
    public class InventoryReport : Controller
    { 
        private readonly IInventoryService _iInventoryService;

        public InventoryReport(IInventoryService iInventoryService)
        {
            _iInventoryService = iInventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APICalls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inv = (await _iInventoryService.GetAllInventoryAsync()).Select(x => new InventoryVm()
            {
                Product_Name=x.Product.Product_Name,
                Type=x.Product.Product_Type.Type_Name,
                Category=x.Product.Product_Category.Category_Name,
                Unit=x.Product.Product_Unit.Unit_Symbol,
                Stock_Quantity=x.StockQuantity,
                ReorderLevel=x.ReorderLevel
            }).ToList();

            return Json(new { data=inv });
        }

        #endregion
    }
}
