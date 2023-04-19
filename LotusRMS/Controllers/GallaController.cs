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
            var galla = _gallaService.GetTodayGalla();
            var gallaVM = new GallaVM();

            if (galla == null)
            {
                ViewBag.Message = "No galla setuped yet";
            }
            else
            {
                gallaVM.GallaId=galla.Id;
                gallaVM.Date = galla.Date;
                gallaVM.Opening_Balance = galla.Opening_Balance;
                gallaVM.Closing_Balance = galla.Closing_Balance;
                gallaVM.Galla_Details = new List<GallaDetailVM>();
                if (galla.Galla_Details != null)
                {
                    foreach(var item in galla.Galla_Details)
                    {
                        var gallaDetailVM = new GallaDetailVM()
                        {
                            Detail_Id=item.Id,
                            Time=item.Time,
                            Title=item.Title,
                            Deposit=item.Deposit,
                            Withdrawl=item.Withdrawl,
                            Balance=item.Balance
                            
                        };
                        gallaVM.Galla_Details.Add(gallaDetailVM);
                    }
                }
            }
            return View(gallaVM);
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
            _notyf.Success("Galla for today setuped successfully...", 5);
            return Ok();
        }
    }
}
