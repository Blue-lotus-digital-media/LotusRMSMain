using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class TableRepository : BaseRepository<LotusRMS_Table>, ITableRepository
    {
        public readonly ApplicationDbContext _dal;
        public TableRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }

        public void Update(LotusRMS_Table table)
        {
            var tables = _dal.LotusRMS_Tables.FirstOrDefault(x => x.Id == table.Id);
            if (tables != null)
            {
                tables.Update(
                    table_Name:table.Table_Name, 
                    table_No:table.Table_No,
                    no_Of_Chair:table.No_Of_Chair,
                    table_Type_Id:table.Table_Type_Id);
                Save();
            }
        }

        public void UpdateStatus(Guid Id)
        {
            var table = _dal.LotusRMS_Tables.FirstOrDefault(x => x.Id == Id);
            if (table != null)
            {
                table.Status = !table.Status;
                Save();
            }
        } 
        public bool UpdateReserved(Guid Id)
        {
            var table = _dal.LotusRMS_Tables.FirstOrDefault(x => x.Id == Id);
            if (table != null)
            {
                table.IsReserved = !table.IsReserved;
                Save();
            }
            return table.IsReserved;
        }
    }
}
