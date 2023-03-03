using LotusRMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.Viewmodels.Category
{
    public class CategoryVM
    {
        public Guid Id { get; set; }
        public string Category_Name { get; set; }

        public string Category_Description { get; set; }
        public Guid Type_Id { get; set; }
        public string Type_Name { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public CategoryVM(string category_Name, string category_Description, Guid type_Id)
        {
            Category_Name = category_Name;
            Category_Description = category_Description;           
            Type_Id = type_Id;
        }

        public CategoryVM()
        {
        }
    }
}
