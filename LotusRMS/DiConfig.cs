using LotusRMS.Models.Service;
using LotusRMS.Models.Service.Implementation;

namespace LotusRMSweb
{
    public static class DiConfig
    {
        public static void UseConfMgmtCore(this IServiceCollection services)
        {
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IMenuTypeService, MenuTypeService>();
            services.AddScoped<IMenuCategoryService, MenuCategoryService>();
            services.AddScoped<ITableTypeService, TableTypeService>();
        }
        }
}
