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
        }
        }
}
