using Dapper;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _dal;
        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext dal)
        {
            _dal = dal;
            ConnectionString = dal.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            _dal.Dispose();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {

            _dal.Database.ExecuteSqlRaw(procedureName, param);
           
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            return _dal.Database.SqlQueryRaw<T>(procedureName, param);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            var result= _dal.Database.SqlQueryRaw<T>(procedureName, param);


            return (T)Convert.ChangeType(result.FirstOrDefault(), typeof(T));
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }
    }
}
