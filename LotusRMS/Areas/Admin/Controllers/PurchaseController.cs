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
    [Authorize(Roles ="Admin,SuperAdmin,Cashier")]
    public class PurchaseController : Controller
    {
        private readonly IProductService _iProductService;
        private readonly ISupplierService _iSupplierService;
        private readonly IPurchaseService _iPurchaseService;

        public PurchaseController(IProductService iProductService, ISupplierService iSupplierService, IPurchaseService iPurchaseService)
        {
            _iProductService = iProductService;
            _iSupplierService = iSupplierService;
            _iPurchaseService = iPurchaseService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ChooseSupplier()
        {
            return PartialView("_SupplierView");
        }
        public IActionResult ChooseProduct()
        {
            return PartialView("_ProductView");
        }

        public IActionResult Purchase(CreatePurchaseVm vm)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
                throw new Exception("Please correct the following errors: " + Environment.NewLine + messages);
            }
            var purchase = new CreatePurchaseDTO()
            {
                
                Purchase_Date = vm.DateAD,
                Bill_Amount = (float)vm.BillTotal,
                Bill_No = vm.BillNo,
                Discount = vm.Discount,
                Discount_Type = vm.Discount_Type,
                Paid_Amount = vm.Paid_Amount,
                Payment_Mode = vm.Payment_Mode,
                Supplier_Id = (Guid)vm.SupplierId,
                PurchaseDetails = new List<CreatePurchaseDetailDTO>()

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
            var id=_iPurchaseService.Create(purchase);


            return RedirectToAction(nameof(Index));
        }

        #region APICalls
        public IActionResult GetProduct()
        {
            var product = _iProductService.GetAllAvailable().Select(pro => new ProductVM()
            {
                Id=pro.Id,
                Product_Name=pro.Product_Name,
                Product_Category=pro.Product_Category.Category_Name,
                Product_Type=pro.Product_Type.Type_Name,
                Product_Unit=pro.Product_Unit.Unit_Symbol
            });

            return Json(new { data = product });
        }  public IActionResult GetSupplier()
        {
            var suppliers = _iSupplierService.GetAllAvailable().Select(sup => new SupplierVM()
            {
                Id=sup.Id,
                FullName=sup.FullName,
                Address=sup.Address,
                Contact=sup.Contact,
                PanOrVat=sup.PanOrVat

            });

            return Json(new { data = suppliers });
        }
        #endregion

    }
}
