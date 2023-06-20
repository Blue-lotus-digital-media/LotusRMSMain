using LotusRMS.Models.Dto.TableDTO;
using LotusRMS.Models.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Service.Implementation
{
    public class TableService : ITableService
    {
        public readonly ITableRepository _ITableRepository;
        public TableService(ITableRepository iTableRepository)
        {
            _ITableRepository = iTableRepository;
        }
        public async Task<Guid> CreateAsync(CreateTableDTO dto)
        {
            var table = new LotusRMS_Table(
                table_Name: dto.Table_Name,
                table_No: dto.Table_No,
                no_Of_Chair:dto.No_Of_Chair,
                table_Type_Id: dto.Table_Type_Id);
           await _ITableRepository.AddAsync(table).ConfigureAwait(false);
            await _ITableRepository.SaveAsync().ConfigureAwait(false);
            return table.Id;
        }

        public async Task<IEnumerable<LotusRMS_Table>> GetAllAsync()
        {
            return await _ITableRepository.GetAllAsync(includeProperties: "Table_Type").ConfigureAwait(false);
        } 
        public async Task<IEnumerable<LotusRMS_Table>> GetAllAvailableAsync()
        {
            return  await _ITableRepository.GetAllAsync(x=>!x.IsDelete, includeProperties: "Table_Type").ConfigureAwait(false);
        } 
        public async Task<IEnumerable<LotusRMS_Table>> GetAllReservedAsync()
        {
            return await _ITableRepository.GetAllAsync(x=>!x.IsDelete && x.IsReserved, includeProperties: "Table_Type").ConfigureAwait(false);
        }

        public async Task<IEnumerable<LotusRMS_Table>> GetAllByTypeIdAsync(Guid Id)
        {
            return await _ITableRepository.GetAllAsync(x=>x.Table_Type_Id==Id,includeProperties: "Table_Type").ConfigureAwait(false);
        }

        public async Task<LotusRMS_Table> GetByGuidAsync(Guid Id)
        {
            return await _ITableRepository.GetByGuidAsync(Id).ConfigureAwait(false);
        }
        public async Task<LotusRMS_Table> GetFirstOrDefaultByIdAsync(Guid Id)
        {
            return await _ITableRepository.GetFirstOrDefaultAsync(x=>x.Id==Id,includeProperties:"Table_Type").ConfigureAwait(false);
        }

        public async Task<Guid> UpdateAsync(UpdateTableDTO dto)
        {
            var table = _ITableRepository.GetByGuid(dto.Id) ?? throw new Exception();
            table.Update(
                table_Name: dto.Table_Name,
                table_No: dto.Table_No,
                no_Of_Chair: dto.No_Of_Chair, 
                table_Type_Id:dto.Table_Type_Id);
            await _ITableRepository.UpdateAsync(table);
            return table.Id;
        }

        public async Task<Guid> UpdateStatusAsync(Guid Id)
        {
            await _ITableRepository.UpdateStatusAsync(Id).ConfigureAwait(false);

            return Id;
        }
        public async Task<bool> UpdateReservedAsync(Guid Id)
        {
            var state= await _ITableRepository.UpdateReservedAsync(Id).ConfigureAwait(false);

            return state;
        }

        public async Task<bool> IsDuplicateName(string name,Guid Id=new Guid())
        {
            var table = await _ITableRepository.GetFirstOrDefaultAsync(x => x.Table_Name == name).ConfigureAwait(false);
            if (table == null)
            {
                return false;
            }
            else
            {
                if (Id != Guid.Empty && Id == table.Id)
                {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    }
}
