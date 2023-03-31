using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models.Dto.TypeDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperAdmin")]
    public class MenuTypeController : Controller
    {
        public readonly IMenuTypeService _IMenuTypeService;

        private readonly INotyfService _notyf;

        public MenuTypeController(IMenuTypeService iMenuTypeService, INotyfService notyf)
        {
            _IMenuTypeService = iMenuTypeService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string? returnUrl)
        {
            returnUrl ??= nameof(Index);
            ViewBag.ReturnUrl = returnUrl;


            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTypeVM type, string? returnUrl)
        {
            returnUrl ??= nameof(Index);
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(type);
            }

            var createDto = new CreateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description);

            _IMenuTypeService.Create(createDto);
            _notyf.Success("Menu Type created successfully !", 5);


            return Redirect(returnUrl);
        }

        public IActionResult Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var type = _IMenuTypeService.GetByGuid((Guid)Id);

            var updateTypeVM = new UpdateTypeVM()
            {
                Id = type.Id,
                Type_Description = type.Type_Description,
                Type_Name = type.Type_Name
            };

            return View(updateTypeVM);
        }

        [HttpPost]
        public IActionResult Update(UpdateTypeVM type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }

            var dto = new UpdateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description)
            {
                Id = type.Id
            };


            _IMenuTypeService.Update(dto);

            _notyf.Success("Menu Type updated successfully !", 5);
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var types = _IMenuTypeService.GetAll().Select(x => new TypeVM()
            {
                Id = x.Id,
                Type_Name = x.Type_Name,
                Type_Description = x.Type_Description,
                Status = x.Status
            });
            return Json(new { data = types });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var unit = _IMenuTypeService.GetByGuid(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = _IMenuTypeService.UpdateStatus(Id);

                return Ok(unit.Status);
            }
        }

        #endregion
    }
}