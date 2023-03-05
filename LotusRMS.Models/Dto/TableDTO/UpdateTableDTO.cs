using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.TableDTO
{
    public class UpdateTableDTO : CreateTableDTO
    {
        public UpdateTableDTO(string table_Name, int table_No,int no_Of_Chair, Guid table_Type_Id) : base(table_Name, table_No,no_Of_Chair, table_Type_Id)
        {
        }
        public Guid Id { get; set; }
    }
}
