using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task UpdateAsync(LotusRMS_Table table)
        {
            var tables = await GetFirstOrDefaultAsync(x => x.Id == table.Id).ConfigureAwait(false);
            if (tables != null)
            {
                tables.Update(
                    table_Name:table.Table_Name, 
                    table_No:table.Table_No,
                    no_Of_Chair:table.No_Of_Chair,
                    table_Type_Id:table.Table_Type_Id);
                await SaveAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateStatusAsync(Guid Id)
        {
            var table = await GetFirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            if (table != null)
            {
                table.Status = !table.Status;
                await SaveAsync().ConfigureAwait(false);
            }
        } 
        public async Task<bool> UpdateReservedAsync(Guid Id)
        {
           var table = await GetFirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            if (table != null)
            {
                table.IsReserved = !table.IsReserved;
                await SaveAsync().ConfigureAwait(false);
            }
            return table.IsReserved;
        }
    }
}
