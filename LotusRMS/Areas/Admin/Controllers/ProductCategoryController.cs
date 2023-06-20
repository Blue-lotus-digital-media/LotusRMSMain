using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Category;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class ProductCategoryController : Controller
    {
        private readonly ICategoryService _ICategoryService;
        private readonly ITypeService _ITypeService;    
        private readonly INotyfService _notyf;

        public ProductCategoryController(ICategoryService iCategoryService, INotyfService notyf, ITypeService iTypeService)
        {
            _ICategoryService = iCategoryService;
            _notyf = notyf;
            _ITypeService = iTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(string? returnUrl = null)
        {
            returnUrl ??= nameof(Index);
            var category = new CreateCategoryVM();
            var typeList = (await _ITypeService.GetAllAvailableAsync()).Select(x => new SelectListItem()
            {
                Text = x.Type_Name,
                Value = x.Id.ToString()
            });
            category.TypeList = typeList;

            ViewBag.ReturnUrl = returnUrl;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM obj, string? returnUrl = null)
        {
            returnUrl ??= nameof(Index);
            if (!ModelState.IsValid)
            {
                var typeList =(await _ITypeService.GetAllAvailableAsync()).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
                obj.TypeList = typeList;
                _notyf.Error("One or move validation error !!! ", 5);
                return View(obj);
            }
            if (await IsDuplicate(obj.Category_Name))
            {
                obj.TypeList = (await _ITypeService.GetAllAvailableAsync()).Where(x => x.Status).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
                _notyf.Error("Duplicate entry for name " + obj.Category_Name, 5);
                return View(obj);
            }
            var category = new CreateCategoryDTO(obj.Category_Name, obj.Category_Description, obj.Type_Id);

            var id = await _ICategoryService.CreateAsync(category);

            _notyf.Success("Product Category created successfully", 5);
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var updateCategoryVM = new UpdateCategoryVM();
            var typeList = (await _ITypeService.GetAllAvailableAsync()).Select(x => new SelectListItem()
            {
                Text = x.Type_Name,
                Value = x.Id.ToString()
            });
            updateCategoryVM.TypeList = typeList;


            var category =await _ICategoryService.GetByGuidAsync((Guid)Id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            updateCategoryVM.Id = category.Id;
            updateCategoryVM.Category_Name = category.Category_Name;

            updateCategoryVM.Category_Description = category.Category_Description;
            updateCategoryVM.Type_Id = category.Type_Id;


            return View(updateCategoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TypeList = (await _ITypeService.GetAllAvailableAsync()).Where(x => x.Status).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
                return View(vm);
            }
            if (await IsDuplicate(vm.Category_Name,vm.Id))
            {
                vm.TypeList = (await _ITypeService.GetAllAvailableAsync()).Where(x => x.Status).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
                _notyf.Error("Duplicate entry for name " + vm.Category_Name, 5);
                return View(vm);
            }

            var dto = new UpdateCategoryDTO(
                category_Name: vm.Category_Name,
                category_Description: vm.Category_Description,
                type_Id: vm.Type_Id
            )
            {
                Id = vm.Id
            };

            await _ICategoryService.UpdateAsync(dto);

            _notyf.Success("Product Category updated successfully", 5);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsDuplicate(string name,Guid Id=new Guid())
        {
            return await _ICategoryService.IsDuplicateName(name, Id);
        }
        #region API CALLS

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ExportToExcel()
        {
            var arraylist =await _ICategoryService.GetAllAvailableAsync();


            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ProductCategory-" + date + ".xlsx");
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorys = (await _ICategoryService.GetAllAvailableAsync()).Select(x => new CategoryVM()
            {
                Id = x.Id,
                Category_Name = x.Category_Name,
                Category_Description = x.Category_Description,
                Status = x.Status,
                IsDelete = x.IsDelete,
                Type_Name = x.Product_Type.Type_Name
            });
            return Json(new { data = categorys });
        }

        [HttpGet]
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var category = await _ICategoryService.GetByGuidAsync(Id);
            if (category == null)
            {
                return BadRequest();
            }
            else
            {
               await _ICategoryService.UpdateStatusAsync(Id);

                return Ok(category.Status);
            }
        }

        #endregion
    }
}