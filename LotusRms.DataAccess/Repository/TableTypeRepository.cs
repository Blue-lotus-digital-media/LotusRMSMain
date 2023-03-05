using LotusRMS.Models;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess.Repository
{
    public class TableTypeRepository : BaseRepository<LotusRMS_Table_Type>, ITableTypeRepository
    {
        public readonly ApplicationDbContext _dal;
        public TableTypeRepository(ApplicationDbContext dal) : base(dal)
        {
            _dal = dal;
        }
        public void Update(LotusRMS_Table_Type lType)
        {
            var type = _dal.LotusRMS_Table_Types.FirstOrDefault(x => x.Id == lType.Id);
            if (type != null)
            {
                type.Update(lType.Type_Name, lType.Type_Description);
                Save();
            }
        }

        public void UpdateStatus(Guid Id)
        {
            var type = _dal.LotusRMS_Table_Types.FirstOrDefault(x => x.Id == Id);
            if (type != null)
            {
                type.Status = !type.Status;
                Save();
            }
        }
    }
}
