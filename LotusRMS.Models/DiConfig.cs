﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public static class DiConfig
    {
            public static IServiceCollection AddLazyResolution(this IServiceCollection services)
            {
                return services.AddTransient(
                    typeof(Lazy<>),
                    typeof(LazilyResolved<>));
            }
        }

        public class LazilyResolved<T> : Lazy<T>
        {
            public LazilyResolved(IServiceProvider serviceProvider)
                : base(serviceProvider.GetRequiredService<T>)
            {
            }
        }

    
}
