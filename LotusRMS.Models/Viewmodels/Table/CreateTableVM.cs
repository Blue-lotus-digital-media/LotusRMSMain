using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Table
{
    public class CreateTableVM
    {
        public string Table_Name { get; set; }
        public int Table_No { get; set; }

        public int No_Of_Chair { get; set; }
        public Guid Table_Type_Id { get; set; }
        public List<SelectListItem>? Table_Type_List { get; set; }

      

       
    }
}
