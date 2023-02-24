using LotusRMS.DataAccess;
using LotusRMS.Models;
using LotusRMS.Models.Dto.UnitDto;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotusRMSweb.Areas.Admin.Controllers
{
    /*[Authorize(Roles ="Admin")]*/

    [Area("Admin")]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _IUnitOfWork;

        public UnitController(IUnitOfWork iUnitOfWork)
        {
            _IUnitOfWork = iUnitOfWork;
        }

        public IActionResult Index()
        {
            var roles = _IUnitOfWork.Unit.GetAll();


            return View(roles);
        }
        public IActionResult Create() {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UnitVM unitVM)
        {
            if (ModelState.IsValid)
            {
                var unit = new LotusRMS_Unit();
            unit.Update(        
            unit_Name: unitVM.Name,
                    unit_Symbol: unitVM.Unit_Symbol,
                    unit_Description: unitVM.Unit_Description,
                    status: unitVM.Status
                    );

                _IUnitOfWork.Unit.Add(unit);
                _IUnitOfWork.Unit.Save();



                return RedirectToAction("Index");
            }
            else
            {
                return View(unitVM);
            }
        }



    }
}
