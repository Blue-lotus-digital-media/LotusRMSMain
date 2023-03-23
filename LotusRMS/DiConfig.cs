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
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuTypeService, MenuTypeService>();
            services.AddScoped<IMenuUnitService, MenuUnitService>();
            services.AddScoped<IMenuCategoryService, MenuCategoryService>();
            services.AddScoped<ITableTypeService, TableTypeService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IFiscalYearService, FiscalYearService>();
            services.AddScoped<IBillSettingService,BillSettingService >();
            services.AddScoped<ICompanyService,CompanyService >();
            services.AddScoped<IInvoiceService,InvoiceService >();
            services.AddScoped<ICustomerService,CustomerService >();
            services.AddScoped<IUserService,UserService >();
            services.AddScoped<ISupplierService,SupplierService >();
            //services.AddScoped<, >();
        }
        }
}
