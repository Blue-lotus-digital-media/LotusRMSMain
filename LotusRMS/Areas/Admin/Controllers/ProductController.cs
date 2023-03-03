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
        private readonly ITypeService _ITypeService;


        public ProductController(IProductService iProductService, ICategoryService iCategoryService, IUnitService iUnitService, ITypeService iTypeService)
        {
            _IProductService = iProductService;
            _ICategoryService = iCategoryService;
            _IUnitService = iUnitService;
            _ITypeService = iTypeService;
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
                TypeList=_ITypeService.GetAll().Where(x=>x.Status).Select(type=>new SelectListItem()
                {
                    Text=type.Type_Name,
                    Value=type.Id.ToString()
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


                ProductVMs.Id = p.Id;
                ProductVMs.Product_Name = p.Product_Name;
                ProductVMs.Product_Description = p.Product_Description;
                ProductVMs.Product_Category_Id = p.Product_Category_Id;
                ProductVMs.Product_Unit_Id = p.Product_Unit_Id;
                ProductVMs.Product_Type_Id = p.Product_Type_Id;
                ProductVMs.Unit_Quantity = (float)p.Unit_Quantity;

               
               

                return View(ProductVMs);

            }


        }
        [HttpPost]
        public IActionResult UpCreate(ProductVM productVMs)
        {
            if (ModelState.IsValid)
            {
               
                if (productVMs.Id == Guid.Empty)
                {
                    var dto = new CreateProductDTO(
                        product_Name: productVMs.Product_Name,
                        product_Description: productVMs.Product_Description,
                        product_Unit_Id: productVMs.Product_Unit_Id,
                        product_Category_Id: productVMs.Product_Category_Id,
                        unit_Quantity:productVMs.Unit_Quantity,
                        product_Type_Id:productVMs.Product_Type_Id
                        );


                    _IProductService.Create(dto);

                }
                else
                {

                    var products = _IProductService.GetByGuid(productVMs.Id) ?? throw new Exception();
                    if (products == null)
                    {
                        return BadRequest("Product not found");
                    }
                    var dto = new UpdateProductDTO(product_Name: productVMs.Product_Name,
                        product_Description: productVMs.Product_Description,
                        product_Unit_Id: productVMs.Product_Unit_Id,
                        product_Category_Id: productVMs.Product_Category_Id,
                         unit_Quantity: productVMs.Unit_Quantity,
                        product_Type_Id: productVMs.Product_Type_Id

                        )
                    {
                        Id= productVMs.Id
                    };
                    _IProductService.Update(dto);


                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var ProductVMs = new ProductVM()
                {
                    TypeList = _ITypeService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                    {
                        Text = type.Type_Name,
                        Value = type.Id.ToString()
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
            var products = _IProductService.GetAll().ToList().Select(pro => new ProductVM()
            {
                Id=pro.Id,
                Product_Name=pro.Product_Name,
                Product_Description=pro.Product_Description,
                Product_Category_Id=pro.Product_Category_Id,
                Product_Category=pro.Product_Category.Category_Name,
                Product_Unit_Id=pro.Product_Unit_Id,
                Product_Unit=pro.Product_Unit.Unit_Name,
                Product_Type_Id=pro.Product_Type_Id,
                Product_Type=pro.Product_Type.Type_Name,
                Status=pro.Status

            }).ToList();
            return Json(new { data = products });
        }
        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var unit = _IProductService.GetByGuid(Id);
            if (unit == null)
            {
                return BadRequest();

            }
            else
            {

                var id = _IProductService.UpdateStatus(Id);

                return Ok(unit.Status);
            }

        }


        [HttpGet]
        public List<SelectListItem> GetCategory(Guid Id)
        {

            var CategoryList = _ICategoryService.GetAll().Where(x => x.Status && x.Type_Id == Id).Select(type => new SelectListItem()
            {
                Text = type.Category_Name,
                Value = type.Id.ToString()
            }).ToList();
            return CategoryList;

        }
            #endregion

        }
}
