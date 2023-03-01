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
        private readonly IUnitService _IUnitService;
        private readonly IToastNotification _toastNotification;
        public CategoryController(ICategoryService iCategoryService, IToastNotification toastNotification, IUnitService iUnitService)
        {
            _ICategoryService = iCategoryService;
            _toastNotification = toastNotification;
            _IUnitService = iUnitService;   
        }
        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Upcreate(Guid? id)
        {
            var category = new UpdateCategoryVM();

            if (id == null)
            {
                return View(category);
            }
            else
            {

                var cat = _ICategoryService.GetByGuid((Guid)id);
                category = new UpdateCategoryVM()
                {
                    Category_Name = cat.Category_Name,
                    Category_Description = cat.Category_Description,
                    Status = cat.Status,
                    IsDelete = cat.IsDelete
                };




                return View(category);
            }


            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upcreate(UpdateCategoryVM obj)
        {

            if (!ModelState.IsValid)
            {
                return View(obj);

            }
            else
            {
                if (obj.Id == null || obj.Id==Guid.Empty)
                {

                    var category = new CreateCategoryDTO(obj.Category_Name,obj.Category_Description);
                    var id = _ICategoryService.Create(category);

                    return RedirectToAction(nameof(Index));

                }
                else
                {

                    var category = _ICategoryService.GetByGuid(obj.Id);
                    if (category == null)
                    {
                        return BadRequest();

                    }
                    else
                    {
                        var DTO = new UpdateCategoryDTO(category_Name: category.Category_Name,category_Description : category.Category_Description)
                        {
                            Id = obj.Id,
                         
                        };
                        
                       var id = _ICategoryService.Update(DTO);

                        return RedirectToAction(nameof(Index));


                    }
                }

            }
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

            var categorys =_ICategoryService.GetAll().Select(x => new UpdateCategoryVM()
            {
                Id = x.Id,
                Category_Name = x.Category_Name,
                Category_Description = x.Category_Description,
                Status = x.Status


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
