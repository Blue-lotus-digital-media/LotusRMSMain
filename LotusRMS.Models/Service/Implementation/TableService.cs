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
        public async Task<Guid> Create(CreateTableDTO dto)
        {
            var table = new LotusRMS_Table(
                table_Name: dto.Table_Name,
                table_No: dto.Table_No,
                no_Of_Chair:dto.No_Of_Chair,
                table_Type_Id: dto.Table_Type_Id);
            _ITableRepository.Add(table);
            _ITableRepository.Save();
            return table.Id;
        }

        public IEnumerable<LotusRMS_Table> GetAll()
        {
            return _ITableRepository.GetAll(includeProperties: "Table_Type");
        }

        public LotusRMS_Table GetByGuid(Guid Id)
        {
            return _ITableRepository.GetByGuid(Id);
        }

        public Guid Update(UpdateTableDTO dto)
        {
            var table = _ITableRepository.GetByGuid(dto.Id) ?? throw new Exception();
            table.Update(
                table_Name: dto.Table_Name,
                table_No: dto.Table_No,
                no_Of_Chair: dto.No_Of_Chair, 
                table_Type_Id:dto.Table_Type_Id);
            _ITableRepository.Update(table);
            return table.Id;
        }

        public Guid UpdateStatus(Guid Id)
        {
            _ITableRepository.UpdateStatus(Id);

            return Id;
        }
    }
}
