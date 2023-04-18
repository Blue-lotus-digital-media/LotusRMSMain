using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models;
using LotusRMS.Models.Dto.GallaDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Galla;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LotusRMSweb.Controllers
{
    public class GallaController : Controller
    {
        private readonly UserManager<RMSUser> _userManager;
        private readonly IGallaService _gallaService;
        private readonly INotyfService _notyf;

        public GallaController(UserManager<RMSUser> userManager,
            IGallaService gallaService,
            INotyfService notyf)
        {
            _userManager = userManager;
            _gallaService = gallaService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateGalla(CreateGallaVM vm)
        {
            var cashier = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var dto = new CreateGallaDTO()
            {
                Cashier = cashier,
                Opening_Balance = vm.Opening_Balance
            };

            _gallaService.CreateGalla(dto);
            _notyf.Success("Galla for today setuoed successfully...", 5);
            return Ok();
        }
    }
}
