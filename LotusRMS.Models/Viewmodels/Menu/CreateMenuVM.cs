﻿using LotusRMS.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Menu
{
    public class CreateMenuVM
    {
        public string Item_name { get; set; }
        
        public Guid Unit_Id { get; set; }
        public Guid Type_Id { get; set; }
        public Guid Category_Id { get; set; }
        public string OrderTo { get; set; }


        public IFormFile? Menu_Image { get; set; }

        public ICollection<CreateMenuDetailVM> MenuDetail { get; set; } = new List<CreateMenuDetailVM>();   

        public List<SelectListItem>? Menu_Unit_List { get; set; }

        public List<SelectListItem>? Menu_Category_List { get; set; }

        public List<SelectListItem>? Menu_Type_List { get; set; }

        public List<MenuIncredianVM> Menu_Incredian { get; set; } = new List<MenuIncredianVM>();

        public ICollection<SelectListItem>? UnitDivision { get; set; }
      

    }
}
