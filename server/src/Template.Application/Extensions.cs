using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application;
using Template.Application.TestApp;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IElasticService, ElasticService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton<ITestService, TestService>();
            return services;
        }
    }
}
