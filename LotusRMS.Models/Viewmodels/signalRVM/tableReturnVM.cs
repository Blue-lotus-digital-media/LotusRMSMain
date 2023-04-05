using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.signalRVM
{
    public class tableReturnVM
    {
        public Guid Type_Id { get; set; }

        public Guid Table_Id { get; set; }
        public int BookCount { get; set; }
    }
}
