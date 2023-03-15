using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.FiscalYearDTO
{
    public class UpdateFiscalYearDTO:CreateFiscalYearDTO
    {
        public Guid Id { get; set; }
    }
}
