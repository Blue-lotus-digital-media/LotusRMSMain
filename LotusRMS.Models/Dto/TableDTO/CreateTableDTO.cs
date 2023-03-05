using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.TableDTO
{
    public class CreateTableDTO
    {
        public string Table_Name { get; private set; }
        public int Table_No { get; private set; }
        public int No_Of_Chair { get; private set; }
        public Guid Table_Type_Id { get; private set; }
        public LotusRMS_Table_Type? Table_Type { get; set; }

        public CreateTableDTO(string table_Name, int table_No,int no_Of_Chair, Guid table_Type_Id)
        {
            Table_Name = table_Name;
            Table_No = table_No;
            No_Of_Chair = no_Of_Chair;
            Table_Type_Id = table_Type_Id;
        }
    }
}
