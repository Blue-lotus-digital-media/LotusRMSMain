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
        public async Task<IActionResult> Create(CreateTypeVM type, string? returnUrl)
        {
            returnUrl ??= nameof(Index);
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(type);
            }

            var createDto = new CreateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description);

            await _IMenuTypeService.CreateAsync(createDto).ConfigureAwait(true);
            _notyf.Success("Menu Type created successfully !", 5);


            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Update(Guid? Id)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var type = await _IMenuTypeService.GetByGuidAsync((Guid)Id).ConfigureAwait(true);

            var updateTypeVM = new UpdateTypeVM()
            {
                Id = type.Id,
                Type_Description = type.Type_Description,
                Type_Name = type.Type_Name
            };

            return View(updateTypeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTypeVM type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }

            var dto = new UpdateTypeDTO(type_Name: type.Type_Name, type_Description: type.Type_Description)
            {
                Id = type.Id
            };


            await _IMenuTypeService.UpdateAsync(dto).ConfigureAwait(true);

            _notyf.Success("Menu Type updated successfully !", 5);
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var types = (await _IMenuTypeService.GetAllAvailableAsync()).Select(x => new TypeVM()
            {
                Id = x.Id,
                Type_Name = x.Type_Name,
                Type_Description = x.Type_Description,
                Status = x.Status
            });
            return Json(new { data = types });
        }

        [HttpGet]
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var unit =await _IMenuTypeService.GetByGuidAsync(Id).ConfigureAwait(true);
            if (unit == null)
            {
                return BadRequest("No unit found");
            }
            else
            {
                var id = await _IMenuTypeService.UpdateStatusAsync(Id).ConfigureAwait(true);

                return Ok(unit.Status);
            }
        }

        #endregion
    }
}