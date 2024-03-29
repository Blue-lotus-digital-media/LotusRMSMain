﻿using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using LotusRMS.Models.Viewmodels.Order;
using LotusRMS.Utility;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class GallaRepository : BaseRepository<LotusRMS_Galla>, IGallaRepository
    {
        private readonly ApplicationDbContext _dal; 
        public GallaRepository(ApplicationDbContext dal
            ) : base(dal)
        {
            _dal = dal;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task AddNewDetailAsync(LotusRMS_Galla obj)
        {

        }

        public async Task<LotusRMS_Galla?> GetGallaByDateAsync(string Date,string UserId)
        {
            var date = Convert.ToDateTime(Date);
            var galla = await GetFirstOrDefaultAsync(filter: x => x.Date == date && x.Cashier == UserId, includeProperties: "Galla_Details,User").ConfigureAwait(false);
            return galla;
        } 
        public async Task<LotusRMS_Galla?> GetLastGallaAsync(string UserId)
        {
            var galla = await GetLastOrDefaultAsync(filter: x =>x.Cashier == UserId,orderBy:x=>x.OrderBy(y=>y.Date),includeProperties: "Galla_Details,User").ConfigureAwait(false);
            return galla;
        }

        public async Task<LotusRMS_Galla?> GetGallaByGuidAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<LotusRMS_Galla?> GetGallaTodayAsync(string UserId)
        {
            var date =Convert.ToDateTime(CurrentTime.DateTimeToday()); 
            var galla = await GetFirstOrDefaultAsync(filter:x=>x.Date==date&& x.Cashier==UserId, includeProperties: "Galla_Details,User").ConfigureAwait(false);
            return galla;
        }
    }
}
