using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.UnitDto
{
    public class UnitUpdateDto : UnitCreateDto
    {
        public UnitUpdateDto(string unitName, string unitSymbol, string unitDescription, bool status) : base(unitName, unitSymbol, unitDescription, status)
        {
        }

        public Guid Id { get; set; }
    }
}
