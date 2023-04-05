using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Order
{
    public class TableTypeBookedVM
    {
        public Guid Type_Id { get; set; }
        public string Type_Name { get; set; }
        public int BookedCount { get; set; }

    }
}
