using AspNetCoreHero.ToastNotification.Abstractions;
using Irony.Parsing;
using LotusRMS.Models;
using LotusRMS.Models.Dto.PurchaseDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.product;
using LotusRMS.Models.Viewmodels.Purchase;
using LotusRMS.Models.Viewmodels.Supplier;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin,Cashier")]
    public class PurchaseController : Controller
    {
        private readonly IProductService _iProductService;
        private readonly ISupplierService _iSupplierService;
        private readonly IPurchaseService _iPurchaseService;
        private readonly INotyfService _notyf;

        public PurchaseController(IProductService iProductService, ISupplierService iSupplierService,
            IPurchaseService iPurchaseService, INotyfService notyf)
        {
            _iProductService = iProductService;
            _iSupplierService = iSupplierService;
            _iPurchaseService = iPurchaseService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View(new CreatePurchaseVm());
        }

        public IActionResult ChooseSupplier()
        {
            return PartialView("_SupplierView");
        }

        public IActionResult ChooseProduct()
        {
            return PartialView("_ProductView");
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreatePurchaseVm vm)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                return View(vm);
            }

            var purchase = new CreatePurchaseDTO()
            {
                Purchase_Date = vm.DateAD,
                Bill_Amount = vm.BillTotal,
                Bill_No = vm.BillNo,
                Discount = vm.Discount,
                Discount_Type = vm.Discount_Type,
                Paid_Amount = vm.Paid_Amount,
                Payment_Mode = vm.Payment_Mode,
                Supplier_Id = (Guid)vm.SupplierId,
                Due_Amount= vm.Due_Amount
            };
            foreach (var item in vm.ProductList)
            {
                var pd = new CreatePurchaseDetailDTO()
                {
                    Product_Id = item.Product_Id,
                    Product_Quantity = item.Product_Quantity,
                    Product_Rate = item.Product_Rate
                };
                purchase.PurchaseDetails.Add(pd);
            }

            var id = await _iPurchaseService.CreateAsync(purchase);

            _notyf.Success("Purchase completed", 5);
            return RedirectToAction(nameof(Index));
        }

        #region APICalls

        public async Task<IActionResult> GetProduct()
        {
            var product = (await _iProductService.GetAllAvailableAsync()).Select(pro => new ProductVM()
            {
                Id = pro.Id,
                Product_Name = pro.Product_Name,
                Product_Category = pro.Product_Category.Category_Name,
                Product_Type = pro.Product_Type.Type_Name,
                Product_Unit = pro.Product_Unit.Unit_Symbol,
                Product_Unit_Id=pro.Product_Unit_Id
            });

            return Json(new { data = product });
        }

        public IActionResult GetSupplier()
        {
            var suppliers = _iSupplierService.GetAllAvailable().Select(sup => new SupplierVM()
            {
                Id = sup.Id,
                FullName = sup.FullName,
                Address = sup.Address,
                Contact = sup.Contact,
                PanOrVat = sup.PanOrVat
            });

            return Json(new { data = suppliers });
        }

        #endregion
    }
}