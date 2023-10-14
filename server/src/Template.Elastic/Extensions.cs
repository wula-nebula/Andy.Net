using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        //public const string RootKey = "http://es-cn-zz4301nab0004qrqo.public.elasticsearch.aliyuncs.com:9200";
        public const string RootKey = "http://10.248.1.211:9200";
        public const string UserName = "elastic";
        public const string Password = "ZF6yApHZBaYh72ArY2q";
        public static readonly Regex whitespace = new Regex(@"\s+", RegexOptions.Compiled);
        public static IServiceCollection AddElastic(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var entrypoints = whitespace.Split(RootKey).Where(item => item != "").ToArray();
            if (!entrypoints.Any()) throw new Exception("source empty");

            var connectionPoool = new StaticConnectionPool(entrypoints.Select(item => new Uri(item)).ToArray());
            var settings = new ConnectionConfiguration(connectionPoool);
            //settings.BasicAuthentication(UserName, Password);
            services.AddSingleton<IElasticLowLevelClient>(serviceProvider => new ElasticLowLevelClient(settings));
            services.AddSingleton<IElasticLowLevelClient, ElasticLowLevelClient>();
            return services;
        }
        
    }
}

