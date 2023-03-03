using ClosedXML.Excel;
using LotusRMS.Models;
using LotusRMS.Models.Dto.CategoryDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Category;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _ICategoryService;
        private readonly ITypeService _ITypeService;
        private readonly IToastNotification _toastNotification;
        public CategoryController(ICategoryService iCategoryService, IToastNotification toastNotification, ITypeService iTypeService)
        {
            _ICategoryService = iCategoryService;
            _toastNotification = toastNotification;
            _ITypeService = iTypeService;   
        }
        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Create(string? returnUrl=null)
        {
            returnUrl ??= nameof(Index);

            var category = new CreateCategoryVM();
            var typeList = _ITypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
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
                var typeList = _ITypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
                {
                    Text = x.Type_Name,
                    Value = x.Id.ToString()
                });
                obj.TypeList = typeList;

                return View(obj);

            }
              var category = new CreateCategoryDTO(obj.Category_Name,obj.Category_Description,obj.Type_Id);
            
            var id = _ICategoryService.Create(category);


                    return Redirect(returnUrl);

              

        }

        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var updateCategoryVM = new UpdateCategoryVM();
            var typeList = _ITypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
            {
                Text = x.Type_Name,
                Value = x.Id.ToString()
            });
            updateCategoryVM.TypeList = typeList;


            var category = _ICategoryService.GetByGuid((Guid)Id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }


            updateCategoryVM.Category_Name = category.Category_Name;
            
            updateCategoryVM.Category_Description = category.Category_Description;
            updateCategoryVM.Type_Id = category.Type_Id;



            return View(updateCategoryVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateCategoryVM vm)
        {
           
            if (!ModelState.IsValid) {
                vm.TypeList = _ITypeService.GetAll().Where(x => x.Status).Select(x => new SelectListItem()
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
                );

            _ICategoryService.Update(dto);
            return RedirectToAction(nameof(Index));

        }
             

        #region API CALLS
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ExportToExcel()
        {
            var arraylist = _ICategoryService.GetAll();


            using (XLWorkbook xl = new XLWorkbook())
            {
                xl.Worksheets.Add(ArrayToDataTable.ToDataTable(arraylist.ToList()));

                using (MemoryStream mstream = new MemoryStream())
                {
                    xl.SaveAs(mstream);
                    var date = CurrentTime.DateTimeToday();
                    return File(mstream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Category-" + date + ".xlsx");
                }
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {

            var categorys =_ICategoryService.GetAll().Select(x => new CategoryVM()
            {
                Id = x.Id,
                Category_Name = x.Category_Name,
                Category_Description = x.Category_Description,
                Status=x.Status,
                IsDelete=x.IsDelete,
                Type_Name=x.Product_Type.Type_Name



            });
            return Json(new { data = categorys });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var category = _ICategoryService.GetByGuid(Id);
            if (category == null)
            {
                return BadRequest();

            }
            else
            {
              
                _ICategoryService.UpdateStatus(Id);
               
                return Ok(category.Status);
            }

        }

        #endregion

    }
}
