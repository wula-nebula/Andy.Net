using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Template.Core.Common;
using Template.Dapper;
using Template.Remote.HttpApp;
using Template.Web.Common;

namespace Template.Web
{
    public class Startup
    {
        private const string conn = "server=localhost;port=3306;database=yt_oms;uid=root;pwd=123456;Charset=utf8;Allow User Variables=True;SslMode=none;";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddHttpClient();
            services.AddHttpClient("template").ConfigureHttpClient(option =>
            {
                option.DefaultRequestHeaders.Connection.Add("keep-alive");
            });//.SetHandlerLifetime(TimeSpan.FromMinutes(5));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Template.Web", Version = "v1" });
            });
            services.AddHealthChecks();
            //×¢²á·þÎñ
            services.AddDapper(conn, SqlProvider.MySql)
                .AddApplication()
                .AddHttpProxy()
                .AddElastic();

            //services.AddCap(x =>
            //{
            //    // CAP support RabbitMQ,Kafka,AzureService as the MQ, choose to add configuration you needed£º
            //    x.UseRabbitMQ(x =>
            //    {
            //        x.HostName = "10.100.48.252";
            //        //x.Port = 5672;
            //        x.UserName = "OMSV2";
            //        x.Password = "OMSV2";
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template.Web v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
