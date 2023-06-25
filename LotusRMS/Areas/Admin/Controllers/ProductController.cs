using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.DataAccess.Constants;
using LotusRMS.Models;
using LotusRMS.Models.Dto.InventoryDTO;
using LotusRMS.Models.Dto.ProductDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Inventory;
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
        private readonly INotyfService _notyf;
        private readonly IInventoryService _iInventoryService;


        public ProductController(IProductService iProductService, ICategoryService iCategoryService,
            IUnitService iUnitService, ITypeService iTypeService, INotyfService notyf, 
            IInventoryService iInventoryService)
        {
            _IProductService = iProductService;
            _ICategoryService = iCategoryService;
            _IUnitService = iUnitService;
            _ITypeService = iTypeService;
            _notyf = notyf;
            _iInventoryService = iInventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var CategoryList = await _ICategoryService.GetAllAsync();
            var UnitList = await _IUnitService.GetAllAsync();
            var createProductVMs = new CreateProductVM()
            {
                TypeList = (await _ITypeService.GetAllAvailableAsync()).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                }),


                UnitList = (await _IUnitService.GetAllAvailableAsync()).Select(i => new SelectListItem()
                {
                    Text = i.Unit_Name,
                    Value = i.Id.ToString()
                })
            };
            return View(createProductVMs);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVMs)
        {
            if (ModelState.IsValid)
            {

                var dto = new CreateProductDTO(
                    product_Name: productVMs.Product_Name,
                    product_Description: productVMs.Product_Description,
                    product_Unit_Id: productVMs.Product_Unit_Id,
                    product_Category_Id: productVMs.Product_Category_Id,
                    unit_Quantity: productVMs.Unit_Quantity,
                    product_Type_Id: productVMs.Product_Type_Id
                );


                var id=await _IProductService.CreateAsync(dto);
                var invDto = new CreateInventoryDTO()
                {
                    ProductId = id,
                    StockQuantity = productVMs.Inventory.StockQuantity,
                    ReorderLevel = productVMs.Inventory.ReorderLevel
                };
                await _iInventoryService.CreateAsync(invDto);
                _notyf.Success("Product created successfully", 5);



                return RedirectToAction(nameof(Index));
            }
            else
            {

                productVMs.TypeList = (await _ITypeService.GetAllAvailableAsync()).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                });
                productVMs.CategoryList =await GetCategory(productVMs.Product_Type_Id);

                productVMs.UnitList =(await _IUnitService.GetAllAvailableAsync()).Select(i => new SelectListItem()
                {
                    Text = i.Unit_Name,
                    Value = i.Id.ToString()
                });

                return View(productVMs);
            }
            
        }
        public async Task<IActionResult> Update(Guid? Id)
        {
            if (Id == Guid.Empty || Id == null) {
                return BadRequest();
            }
            var CategoryList =await _ICategoryService.GetAllAvailableAsync();
            var UnitList = await _IUnitService.GetAllAvailableAsync();
            var updateProductVMs = new UpdateProductVM()
            {
                TypeList = (await _ITypeService.GetAllAvailableAsync()).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                }),


                UnitList = UnitList.Where(x => x.Status).Select(i => new SelectListItem()
                {
                    Text = i.Unit_Name,
                    Value = i.Id.ToString()
                })
            };


                       var p =await _IProductService.GetByGuidAsync((Guid)Id) ?? throw new Exception();
            updateProductVMs.Id = p.Id;
            updateProductVMs.Product_Name = p.Product_Name;
            updateProductVMs.Product_Description = p.Product_Description;
            updateProductVMs.Product_Category_Id = p.Product_Category_Id;
            updateProductVMs.CategoryList =await GetCategory(p.Product_Type_Id);
            updateProductVMs.Product_Unit_Id = p.Product_Unit_Id;
            updateProductVMs.Product_Type_Id = p.Product_Type_Id;
            updateProductVMs.Unit_Quantity = (double)p.Unit_Quantity;

            updateProductVMs.Inventory = await GetInventory(p.Id);
            return View(updateProductVMs);
            
        }
        public async Task<UpdateInventory> GetInventory(Guid ProductId)
        {
            var inventory =await _iInventoryService.GetInventoryByProductIdAsync(ProductId);
            var inv = new UpdateInventory();
            if (inventory != null)
            {
                inv.Id = inventory.Id;
                inv.Product_Id = inventory.Product_Id;
                inv.StockQuantity = inventory.StockQuantity;
                inv.ReorderLevel = inventory.ReorderLevel;
                inv.IsPurchased = inventory.IsPurchased;
            }
            return inv;
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM productVMs)
        {
            if (ModelState.IsValid)
            {
              
                    var products = await _IProductService.GetByGuidAsync(productVMs.Id) ?? throw new Exception();
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
                        Id = productVMs.Id
                    };
               await _IProductService.UpdateAsync(dto);
                if (productVMs.Inventory.Id == Guid.Empty)
                {
                    var createInv = new CreateInventoryDTO()
                    {
                        ProductId = productVMs.Id,
                        StockQuantity = productVMs.Inventory.StockQuantity,
                        ReorderLevel = productVMs.Inventory.ReorderLevel
                    };
                    await _iInventoryService.CreateAsync(createInv);
                }
                else
                {
                    var updateInv = new UpdateInventoryDTO()
                    {
                        Id = productVMs.Inventory.Id,
                        ProductId = productVMs.Id,
                        StockQuantity = productVMs.Inventory.StockQuantity,
                        ReorderLevel = productVMs.Inventory.ReorderLevel
                    };
                    await _iInventoryService.UpdateAsync(updateInv);
                }
               _notyf.Success("Product updated successfully", 5);
               return RedirectToAction(nameof(Index));
            }
            else
            {
                productVMs.TypeList = (await _ITypeService.GetAllAvailableAsync()).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                });
                productVMs.CategoryList =await GetCategory(productVMs.Product_Type_Id);

                productVMs.UnitList = (await _IUnitService.GetAllAvailableAsync()).Select(i => new SelectListItem()
                {
                    Text = i.Unit_Name,
                    Value = i.Id.ToString()
                });
                
                return View(productVMs);
            }
        }


        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = (await _IProductService.GetAllAsync()).Select(pro => new ProductVM()
            {
                Id = pro.Id,
                Product_Name = pro.Product_Name,
                Product_Description = pro.Product_Description,
                Product_Category_Id = pro.Product_Category_Id,
                Product_Category = pro.Product_Category.Category_Name,
                Product_Unit_Id = pro.Product_Unit_Id,
                Product_Unit = pro.Product_Unit.Unit_Name,
                Product_Type_Id = pro.Product_Type_Id,
                Product_Type = pro.Product_Type.Type_Name,
                Status = pro.Status
            }).ToList();
            return Json(new { data = products });
        }

        [HttpGet]
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var unit = await _IProductService.GetByGuidAsync(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = await _IProductService.UpdateStatusAsync(Id);
                if (unit.Status == true)
                {
                    _notyf.Success("Status Activated successfully..", 2);
                }
                else
                {
                    _notyf.Warning("Status Deactivated...", 2);
                }
                return Ok(unit.Status);

            
            }
        }


        [HttpGet]
        public async Task<List<SelectListItem>> GetCategory(Guid Id)
        {
            var CategoryList = (await _ICategoryService.GetAllAvailableAsync()).Where(x => x.Type_Id == Id).Select(type =>
                new SelectListItem()
                {
                    Text = type.Category_Name,
                    Value = type.Id.ToString()
                }).ToList();
            return CategoryList;
        }

        #endregion
    }
}