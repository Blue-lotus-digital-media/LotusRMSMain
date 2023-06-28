using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models;
using LotusRMS.Models.Dto.GallaDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Viewmodels.Galla;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LotusRMSweb.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Cashier")]
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

        public async Task<IActionResult> Index()
        {
            var galla = await _gallaService.GetTodayGallaAsync().ConfigureAwait(true);
            var gallaVM = new GallaVM();

            if (galla == null)
            {
                ViewBag.Message = "No galla setuped yet";
            }
            else
            {
                gallaVM = GetGallaVM(galla);
            }
            return View(gallaVM);
        }
        public GallaVM GetGallaVM(LotusRMS_Galla galla)
        {
            var gallaVM = new GallaVM()
            {
                GallaId = galla.Id,
                Date = galla.Date,
                Opening_Balance = galla.Opening_Balance,
                Closing_Balance = galla.Closing_Balance,
                 };

            if (galla.Galla_Details != null)
            {
                foreach (var item in galla.Galla_Details)
                {
                    var gallaDetailVM = new GallaDetailVM()
                    {
                        Detail_Id = item.Id,
                        Time = item.Time,
                        Title = item.Title,
                        Deposit = item.Deposit,
                        Withdrawl = item.Withdrawl,
                        Balance = item.Balance
                    };
                    gallaVM.Galla_Details.Add(gallaDetailVM);
                }
            }
            gallaVM.Galla_Details = gallaVM.Galla_Details.OrderByDescending(x => x.Time).ToList();
            return gallaVM;
        
    }

        public async Task<IActionResult> CreateGalla(CreateGallaVM vm)
        {
            var cashier = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var dto = new CreateGallaDTO()
            {
                Cashier = cashier,
                Opening_Balance = vm.Opening_Balance
            };
            await _gallaService.CreateGallaAsync(dto).ConfigureAwait(true);
            _notyf.Success("Galla for today setuped successfully...", 5);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> WithDrawGalla(double withDrawAmount)
        {
            var galla = await _gallaService.GetTodayGallaAsync().ConfigureAwait(true);
            if (galla != null)
            {
                var closing = galla.Closing_Balance - withDrawAmount;
                var dto = new AddGallaDetailDTO()
                {
                    Galla_Id = galla.Id,
                    Closing_Balance=closing,
                    GallaDetail=new CreateGallaDetailVM()
                    {
                        Time=CurrentTime.DateTimeNow(),
                        Title="Withdrawl by "+ User.FindFirstValue("firstname") + " " + User.FindFirstValue("middlename") + " " + User.FindFirstValue("lastname"),
                        Withdrawl=withDrawAmount,
                        Deposit=0
                    }
                };
                await _gallaService.AddGallaDetailAsync(dto).ConfigureAwait(true);
                _notyf.Success("Galla withdrawl successfully...", 5);
            }
            else
            {
                _notyf.Error("No Galla Setuped yet today!!!", 5);
            }
            return Ok(GetGallaVM(galla));
        }
    }
}
