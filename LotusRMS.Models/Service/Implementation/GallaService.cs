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

        public async Task AddGallaDetailAsync(AddGallaDetailDTO dto)
        {
            var galla =await _gallaRepository.GetByGuidAsync(dto.Galla_Id).ConfigureAwait(false);
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
            await _gallaRepository.SaveAsync();
        }

        public async Task CreateGallaAsync(CreateGallaDTO dto)
        {
            var galla = new LotusRMS_Galla()
            {
                Cashier = dto.Cashier,
                Date = Convert.ToDateTime(CurrentTime.DateTimeToday()),
                Opening_Balance = dto.Opening_Balance,
                Closing_Balance = dto.Opening_Balance,
            };

            await _gallaRepository.AddAsync(galla).ConfigureAwait(false);
        }

        public async Task<LotusRMS_Galla?> GetGallaByDateAsync(string date,string UserId)
        {            
            var galla = await _gallaRepository.GetGallaByDateAsync(date,UserId).ConfigureAwait(false);
            return galla;
        }

        public async Task<double> GetGallaAmountAsync()
        {
            var galla = await GetTodayGallaAsync().ConfigureAwait(false);
            if (galla != null)
            {
                return galla.Closing_Balance;
            }
            return 0.0;
        }

        public async Task<LotusRMS_Galla?> GetLastGallaAsync(string UserId)
        {

            //var UserId = httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var galla =await _gallaRepository.GetLastGallaAsync(UserId).ConfigureAwait(false);
            return galla;
        }

        public async Task<LotusRMS_Galla?> GetTodayGallaAsync()
        {
            var UserId= httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var galla =await _gallaRepository.GetGallaTodayAsync(UserId).ConfigureAwait(false);
            return galla;
        }

        public async Task UpdateGallaDetailAsync()
        {
            throw new NotImplementedException();
        }
    }
}
