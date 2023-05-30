using LotusRMS.Models.Viewmodels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.QrTable
{
    public class QrTableMenuVM

    {
        public string Customer_Name { get; set; }
        public string Contact_No { get; set; }
        public string Table_Name { get; set; }
        public int Table_No { get; set; }
        public Guid Table_Id { get; set; }
        public ICollection<MenuVM> Menus { get; set; }
    }
}
