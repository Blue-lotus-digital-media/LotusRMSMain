﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Dto.UnitDto
{
    public class UnitCreateDto
    {
        public string UnitName { get; set; }
        public string UnitSymbol { get; set; }
        public string UnitDescription { get; set; }
       

        public UnitCreateDto(string unitName,string unitSymbol, string unitDescription)
        {
            UnitName = unitName;
            UnitSymbol = unitSymbol;
            UnitDescription = unitDescription;
           
        }

    }
}
