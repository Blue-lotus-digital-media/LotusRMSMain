

using LotusRMS.Utility;

namespace LotusRMS.Models.Viewmodels.product
{
    public class ProductVM
    {
        public UpdateProductVM Product { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        public IEnumerable<SelectListItem>? UnitList { get; set; }
    }
}
