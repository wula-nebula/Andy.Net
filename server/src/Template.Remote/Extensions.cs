using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Remote.HttpApp;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddHttpProxy(this IServiceCollection services)
        {
            services.AddScoped<IHttpClientProxy, HttpClientProxy>();
            return services;
        }
    }
}
