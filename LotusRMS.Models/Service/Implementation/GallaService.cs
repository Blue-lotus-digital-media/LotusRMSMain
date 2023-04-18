using LotusRMS.Models.Dto.GallaDTO;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class GallaService:IGallaService
    {
        private readonly IGallaRepository _gallaRepository;
        private readonly IHttpContextAccessor httpAccessor;

        public GallaService(IGallaRepository gallaRepository,
            IHttpContextAccessor httpAccessor)
        {
            _gallaRepository = gallaRepository;
            this.httpAccessor = httpAccessor;
        }

        public void AddGallaDetail(AddGallaDetailDTO dto)
        {
            var galla = _gallaRepository.GetByGuid(dto.Galla_Id);
            var gallaDetail = new LotusRMS_GallaDetail()
            {
                Time = CurrentTime.DateTimeNow(),
                Title=dto.GallaDetail.Title,
                Deposit = dto.GallaDetail.Deposit,
                Withdrawl = dto.GallaDetail.Withdrawl,
                Balance = galla.Closing_Balance + dto.GallaDetail.Deposit - dto.GallaDetail.Withdrawl
            };
            galla.Closing_Balance = gallaDetail.Balance;
            galla.Galla_Details.Add(gallaDetail);
            _gallaRepository.Save();



           
        }

        public void CreateGalla(CreateGallaDTO dto)
        {
            var galla = new LotusRMS_Galla()
            {
                Cashier = dto.Cashier,
                Date = Convert.ToDateTime(CurrentTime.DateTimeToday()),
                Opening_Balance = dto.Opening_Balance,
                Closing_Balance = dto.Opening_Balance,
            };

            _gallaRepository.Add(galla);
            _gallaRepository.Save();
        }

        public LotusRMS_Galla GetGallaByDate(string date,string UserId)
        {
            
            //var UserId = httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var galla = _gallaRepository.GetGallaByDate(date,UserId);
            return galla;
        }

        public LotusRMS_Galla GetTodayGalla()
        {
            var UserId= httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var galla = _gallaRepository.GetGallaToday(UserId);
            return galla;
        }

        public void UpdateGallaDetail()
        {
            throw new NotImplementedException();
        }
    }
}
