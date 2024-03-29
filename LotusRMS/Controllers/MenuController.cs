﻿using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Menu;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _iMenuService;
        private readonly INotyfService _notyf;
        public MenuController(IMenuService iMenuService,
            INotyfService notyf)
        {
            _iMenuService = iMenuService;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APICall
        public async Task<IActionResult> GetAll()
        {
            var menus = (await _iMenuService.GetAllAvailableAsync().ConfigureAwait(true)).Select(menu => new MenuVM()
            {
                Id = menu.Id,
                Item_name = menu.Item_Name,
                OrderTo = menu.OrderTo,
                Menu_Unit_Name = menu.Menu_Unit.Unit_Symbol,
                Menu_Category_Name = menu.Menu_Category.Category_Name,
                Menu_Type_Name = menu.Menu_Type.Type_Name,
                Menu_Image = ImageUpload.GetStrigFromByteArray(menu.Image),
                Status = menu.Status,
                MenuDetail = menu.Menu_Details.Select(md => new MenuDetailVM()
                {
                    Id = md.Id,
                    Quantity = md.Divison.Title + "(" + md.Divison.Value + " " + menu.Menu_Unit.Unit_Symbol + ")",
                    Rate = md.Rate,
                    IsDefault = md.Default
                }).ToList()
            }).ToList();
            return Json(new { data = menus });
        }
        [HttpGet]
        public async Task<IActionResult> StatusChange(Guid Id)
        {
            var unit =await  _iMenuService.GetByGuidAsync(Id).ConfigureAwait(true);
            if (unit == null)
            {
                return BadRequest("No such unit");
            }
            else
            {
                var id = await _iMenuService.UpdateStatusAsync(Id).ConfigureAwait(true);
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
        #endregion
    }
}
