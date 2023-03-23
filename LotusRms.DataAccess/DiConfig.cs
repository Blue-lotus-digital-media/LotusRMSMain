using LotusRMS.DataAccess.Repository;
using LotusRMS.Models.IRepositorys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.DataAccess
{
    public static class DiConfig
    {
        public static void UseConfMgmtData(this IServiceCollection services)
        {

            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IMenuTypeRepository, MenuTypeRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuUnitRepository, MenuUnitRepository>();
            services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
            services.AddScoped<ITableTypeRepository, TableTypeRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICheckoutRepository, CheckoutRepository>();
            services.AddScoped<IFiscalYearRepository, FiscalYearRepository>();
            services.AddScoped<IBillSettingRepository,BillSettingRepository >();
            services.AddScoped<ICompanyRepository,CompanyRepository >();
            services.AddScoped<IInvoiceRepository,InvoiceRepository >();
            services.AddScoped<ICustomerRepository,CustomerRepository >();
            services.AddScoped<IUserRepository,UserRepository >();
            services.AddScoped<ISupplierRepository,SupplierRepository >();
            //services.AddScoped<, >();
        }
        }
}
