﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.UnitDto
{
    public class UnitUpdateDto : UnitCreateDto
    {
        public UnitUpdateDto(string unitName, string unitSymbol, string unitDescription) : base(unitName, unitSymbol, unitDescription)
        {
        }

        public Guid Id { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
    }
}
