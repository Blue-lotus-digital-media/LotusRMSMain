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

        public LotusRMS_Galla GetGallaByDate(string Date,string UserId)
        {
            var date = Convert.ToDateTime(Date);
            var galla = GetFirstOrDefault(filter: x => x.Date == date && x.Cashier == UserId, includeProperties: "Galla_Details,User");
            return galla;
        } 
        public LotusRMS_Galla GetLastGalla(string UserId)
        {
            var galla = GetLastOrDefault(filter: x =>x.Cashier == UserId,orderBy:x=>x.OrderBy(y=>y.Date),includeProperties: "Galla_Details,User");
            return galla;
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
