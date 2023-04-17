using LotusRMS.Models;
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

        public void AddNewDetail(LotusRMS_Galla obj)
        {

        }

        public LotusRMS_Galla GetGallaByDate(string Date)
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Galla GetGallaByGuid(Guid Id)
        {
            throw new NotImplementedException();
        }

        public LotusRMS_Galla GetGallaToday(string UserId)
        {
            var date =Convert.ToDateTime(CurrentTime.DateTimeToday()); 
            var galla = GetFirstOrDefault(filter:x=>x.Date==date&& x.Cashier==UserId, includeProperties: "Galla_Details,User");
            return galla;
        }
    }
}
