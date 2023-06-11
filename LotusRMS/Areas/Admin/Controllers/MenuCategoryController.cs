using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Category;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cashier,Superadmin")]
    public class MenuCategoryController : Controller
    {
        private readonly IMenuCategoryService _IMenuCategoryService;
        private readonly IMenuTypeService _IMenuTypeService;
        private readonly INotyfService _notyf;

        public MenuCategoryController(IMenuCategoryService iMenuCategoryService,
            INotyfService notyf, IMenuTypeService iMenuTypeService)
        {
            _IMenuCategoryService = iMenuCategoryService;
            _notyf = notyf;
            _IMenuTypeService = iMenuTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string? returnUrl = null)
        {
            returnUrl ??= nameof(Index);

            var category = new CreateCategoryVM();
            var typeList = _IMenuTypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
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
        public IActionResult Create(CreateCategoryVM obj, string? returnUrl = null)
        {
            returnUrl ??= nameof(Index);
            if (!ModelState.IsValid)
            {
                var typeList = _IMenuTypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
                obj.TypeList = typeList;

                _notyf.Error("Some form validation required!", 5);
                return View(obj);
            }

            var category = new CreateCategoryDTO(obj.Category_Name, obj.Category_Description, obj.Type_Id);

            var id = _IMenuCategoryService.Create(category);

            _notyf.Success("Menu category created successfully !", 5);

            return Redirect(returnUrl);
        }

        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                _notyf.Warning("Please selece valid category !", 5);
                return RedirectToAction(nameof(Index));
            }

            var updateCategoryVM = new UpdateCategoryVM();
            var typeList = _IMenuTypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
            {
                Text = x.Type_Name,
                Value = x.Id.ToString()
            });
            updateCategoryVM.TypeList = typeList;


            var category = _IMenuCategoryService.GetByGuid((Guid)Id);
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
        public IActionResult Update(UpdateCategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TypeList = _IMenuTypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
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

            _IMenuCategoryService.Update(dto);
            _notyf.Success("Menu category updated successfully !", 5);
            
            return RedirectToAction(nameof(Index));
        }


        #region API CALLS

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _IMenuCategoryService.GetAll();


            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "MenuCategory-" + date + ".xlsx");
                }
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var categorys = _IMenuCategoryService.GetAll().Select(x => new CategoryVM()
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
        public IActionResult StatusChange(Guid Id)
        {
            var category = _IMenuCategoryService.GetByGuid(Id);
            if (category == null)
            {
                return BadRequest();
            }
            else
            {
                _IMenuCategoryService.UpdateStatus(Id);

                return Ok(category.Status);
            }
        }

        #endregion
    }
}