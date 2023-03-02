using LotusRMS.DataAccess.Constants;
using LotusRMS.Models;
using LotusRMS.Models.Dto.ProductDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.product;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class ProductController : Controller
    {
        private readonly IProductService _IProductService;
        
        private readonly ICategoryService _ICategoryService;   
        private readonly IUnitService _IUnitService;


        public ProductController(IProductService iProductService, ICategoryService iCategoryService, IUnitService iUnitService)
        {
            _IProductService = iProductService;
            _ICategoryService = iCategoryService;
            _IUnitService = iUnitService;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Upcreate(Guid? Id)
        {
            var CategoryList = _ICategoryService.GetAll();
            var UnitList = _IUnitService.GetAll();
            var ProductVMs = new ProductVM()
            {
                Product = new UpdateProductVM(),

                CategoryList = _ICategoryService.GetAll().Where(x=>x.Status).Select(i => new SelectListItem()
                {
                    Text = i.Category_Name,
                    Value = i.Id.ToString()

                }),
                UnitList = _IUnitService.GetAll().Where(x => x.Status).Select(i => new SelectListItem()
                {
                    Text = i.Unit_Name,
                    Value = i.Id.ToString()

                })
            };


            if (Id==Guid.Empty || Id==null )
            {

                return View(ProductVMs);


            }
            else
            {
                var p = _IProductService.GetByGuid((Guid)Id) ?? throw new Exception();
                var updateProductViewmodel = new UpdateProductVM()
                {
                    Id = p.Id,
                    Product_Name = p.Product_Name,
                    Product_Description = p.Product_Description,
                    
                    Product_Category_Id = p.Product_Category_Id,
                    Product_Unit_Id = p.Product_Unit_Id

                };
                ProductVMs.Product = updateProductViewmodel;


                return View(ProductVMs);

            }


        }
        [HttpPost]
        public IActionResult UpCreate(ProductVM productVMs)
        {
            if (ModelState.IsValid)
            {
                var product = productVMs.Product;
                if (productVMs.Product.Id == Guid.Empty)
                {
                    var dto = new CreateProductDTO(
                        product_Name: product.Product_Name,
                        product_Description: product.Product_Description,
                        product_Unit_Id: product.Product_Unit_Id,
                        product_Category_Id: product.Product_Category_Id);

                    _IProductService.Create(dto);

                }
                else
                {

                    var products = _IProductService.GetByGuid(product.Id) ?? throw new Exception();
                    if (products == null)
                    {
                        return BadRequest("Product not found");
                    }
                    var dto = new UpdateProductDTO(product_Name: product.Product_Name,
                        product_Description: product.Product_Description,
                        product_Unit_Id: product.Product_Unit_Id,
                        product_Category_Id: product.Product_Category_Id)
                    {
                        Id=product.Id
                    };
                    _IProductService.Update(dto);


                }

                return RedirectToAction("Index");
            }
            else
            {
                var ProductVMs = new ProductVM()
                {
                    Product = productVMs.Product,
                    CategoryList = _ICategoryService.GetAll().Where(x=>x.Status).Select(i => new SelectListItem()
                    {
                        Text = i.Category_Name,
                        Value = i.Id.ToString()

                    }),
                    UnitList = _IUnitService.GetAll().Select(i => new SelectListItem()
                    {
                        Text = i.Unit_Name,
                        Value = i.Id.ToString()

                    }),
                };
                return View(ProductVMs);
            }
        } 
             


        #region Api Call

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _IProductService.GetAll().ToList().Select(pro => new UpdateProductVM()
            {
                Id=pro.Id,
                Product_Name=pro.Product_Name,
                Product_Description=pro.Product_Description,
                Status=pro.Status,
                IsDelete=pro.IsDelete,
                Product_Category_Id=pro.Product_Category_Id,
                Product_Category=pro.Product_Category.Category_Name,
                Product_Unit_Id=pro.Product_Unit_Id,
                Product_Unit=pro.Product_Unit.Unit_Name

            }).ToList();
            return Json(new { data = products });
        }

            #endregion

        }
}
