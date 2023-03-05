using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Table
{
    public class TableVM
    {
        public Guid Id { get; set; }
        public string Table_Name { get;  set; }
        public int Table_No { get;  set; }
        public int No_Of_Chair { get; set; }
        public bool IsReserved { get; set; } 
        public Guid Table_Type_Id { get;  set; }
        
        public string Table_Type_Name { get; set; }
        public bool Status { get; set; } 
        public bool IsDelete { get; set; } 

    }
}
