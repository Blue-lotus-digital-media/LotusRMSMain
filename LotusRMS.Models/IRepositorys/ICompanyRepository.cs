﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.IRepositorys
{
    public interface ICompanyRepository:IBaseRepository<LotusRMS_Company>
    {
        void Update(LotusRMS_Company obj);
       
    }
}
