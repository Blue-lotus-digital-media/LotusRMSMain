﻿using AspNetCoreHero.ToastNotification.Abstractions;
using LotusRMS.Models.Dto.MenuDTO;
using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;
using LotusRMS.Models.Viewmodels.Menu;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _IMenuService;
        private readonly IMenuTypeService _IMenuTypeService;
        private readonly IMenuCategoryService _IMenuCategoryService;
        private readonly IMenuUnitService _IMenuUnitService;
        private readonly INotyfService _notyf;
        private readonly IUnitService _IUnitService;


        public MenuController(IMenuService iMenuService, IMenuTypeService iMenuTypeService,
            IMenuCategoryService iMenuCategoryService, IMenuUnitService iMenuUnitService, INotyfService notyf,
            IUnitService IUnitService)
        {
            _IMenuService = iMenuService;
            _IMenuTypeService = iMenuTypeService;
            _IMenuCategoryService = iMenuCategoryService;
            _IMenuUnitService = iMenuUnitService;
            _notyf = notyf;
            _IUnitService = IUnitService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var createMenuVM = new CreateMenuVM()
            {
                Menu_Type_List = _IMenuTypeService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                }).ToList(),
                Menu_Unit_List = _IMenuUnitService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Unit_Symbol,
                    Value = type.Id.ToString()
                }).ToList()
            };

           /* ViewBag.UnitList = _IUnitService.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Unit_Symbol,
                Value = i.Id.ToString()
            }).ToList();*/
            return View(createMenuVM);
        }

        [HttpPost]
        public IActionResult Create(CreateMenuVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Menu_Type_List = _IMenuTypeService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                }).ToList();
                vm.Menu_Category_List = _IMenuCategoryService.GetAll().Where(x => x.Status && x.Type_Id == vm.Type_Id)
                    .Select(type => new SelectListItem()
                    {
                        Text = type.Category_Name,
                        Value = type.Id.ToString()
                    }).ToList();
                vm.Menu_Unit_List = _IMenuUnitService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Unit_Symbol,
                    Value = type.Id.ToString()
                }).ToList();


                return View(vm);
            }

            var dto = new CreateMenuDTO()
            {
                Item_name = vm.Item_name,
                OrderTo = vm.OrderTo,
                Unit_Id = vm.Unit_Id,
                Category_Id = vm.Category_Id,
                Type_Id = vm.Type_Id,
                Menu_Details=new List<CreateMenuDetailDTO>()

            };
            foreach(var item in vm.MenuDetail)
            {
                var detail = new CreateMenuDetailDTO()
                {
                    Quantity = item.Quantity,
                    Rate = item.Rate,
                    Default = item.IsDefault
                };
                dto.Menu_Details.Add(detail);
            }
            if (vm.Menu_Image != null)
            {
                dto.Image = ImageUpload.GetByteArrayFromImage(vm.Menu_Image);
            }

            var id = _IMenuService.Create(dto);

            _notyf.Success("Menu Created successfully..", 5);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _notyf.Warning("Error while loading data...");
                return RedirectToAction(nameof(Index));
            }

            var menu = _IMenuService.GetByGuid(Id);
            if (menu == null)
            {
                _notyf.Information("no such data...");
                return RedirectToAction(nameof(Index));
            }

            var updateMenuVM = new UpdateMenuVM()
            {
                Id = menu.Id,
                Item_name = menu.Item_Name,
                Unit_Id = menu.Unit_Id,
                Type_Id = menu.Type_Id,
                Category_Id = menu.Category_Id,
                OrderTo = menu.OrderTo,

                Menu_Type_List = _IMenuTypeService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                }).ToList(),
                Menu_Category_List = _IMenuCategoryService.GetAll().Where(x => x.Status && x.Type_Id == menu.Type_Id)
                    .Select(type => new SelectListItem()
                    {
                        Text = type.Category_Name,
                        Value = type.Id.ToString()
                    }).ToList(),
                Menu_Unit_List = _IMenuUnitService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Unit_Symbol,
                    Value = type.Id.ToString()
                }).ToList()
            };
            return View(updateMenuVM);
        }

        [HttpPost]
        public IActionResult Update(UpdateMenuVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Menu_Type_List = _IMenuTypeService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Type_Name,
                    Value = type.Id.ToString()
                }).ToList();
                vm.Menu_Category_List = _IMenuCategoryService.GetAll().Where(x => x.Status && x.Type_Id == vm.Type_Id)
                    .Select(type => new SelectListItem()
                    {
                        Text = type.Category_Name,
                        Value = type.Id.ToString()
                    }).ToList();
                vm.Menu_Unit_List = _IMenuUnitService.GetAll().Where(x => x.Status).Select(type => new SelectListItem()
                {
                    Text = type.Unit_Symbol,
                    Value = type.Id.ToString()
                }).ToList();
                return View(vm);
            }

            var dto = new UpdateMenuDTO()
            {
                Id = vm.Id,
                Item_name = vm.Item_name,
                OrderTo = vm.OrderTo,
                Unit_Id = vm.Unit_Id,
                Category_Id = vm.Category_Id,
                Type_Id = vm.Type_Id,
            };
            if (vm.Menu_Image != null)
            {
                dto.Image = ImageUpload.GetByteArrayFromImage(vm.Menu_Image);
            }

            _IMenuService.Update(dto);

            _notyf.Success("Menu Updated successfully..", 5);
            return RedirectToAction(nameof(Index));
        }


        #region Api Call

        [HttpGet]
        public IActionResult GetAll()
        {
            var menus = _IMenuService.GetAll().ToList().Select(menu => new MenuVM()
            {
                Id = menu.Id,
                Item_name = menu.Item_Name,
                OrderTo = menu.OrderTo,
                Menu_Unit_Name = menu.Menu_Unit.Unit_Symbol,
                Menu_Category_Name = menu.Menu_Category.Category_Name,
                Menu_Type_Name = menu.Menu_Type.Type_Name,
                Menu_Image = ImageUpload.GetStrigFromByteArray(menu.Image),
                Status = menu.Status
            }).ToList();
            return Json(new { data = menus });
        }

        [HttpGet]
        public IActionResult StatusChange(Guid Id)
        {
            var unit = _IMenuService.GetByGuid(Id);
            if (unit == null)
            {
                return BadRequest();
            }
            else
            {
                var id = _IMenuService.UpdateStatus(Id);
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


        [HttpGet]
        public List<SelectListItem> GetCategory(Guid Id)
        {
            var CategoryList = _IMenuCategoryService.GetAll().Where(x => x.Status && x.Type_Id == Id).Select(type =>
                new SelectListItem()
                {
                    Text = type.Category_Name,
                    Value = type.Id.ToString()
                }).ToList();
            return CategoryList;
        }

        #endregion
    }
}