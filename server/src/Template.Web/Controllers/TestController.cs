using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Template.Application;
using Template.Application.TestApp;
using Template.Core.Common;
using Template.Remote;
using Template.Remote.HttpApp;
using Template.Static;

namespace Template.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private IServiceProvider serviceProvider;
        public TestController(IServiceProvider ServiceProvider)
        {
            serviceProvider = ServiceProvider;
        }
        /// <summary>
        /// IHttpClientFactory测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> StaticRequestUnit()
        {
            var result = await HttpHelper.Request<string>("https://www.baidu.com/", default, HttpMethod.Get);
            return result;
        }
        /// <summary>
        /// IHttpClientFactory测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> FactoryRequestUnit()
        {
            var httpServ = serviceProvider.GetRequiredService<IHttpClientProxy>();
            var result = await httpServ.Request<string>("https://www.toutiao.com", default, HttpMethod.Get);
            return result;
        }
       
        /// <summary>
        /// 正则分割测试
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public string RegexSplitUnit(string param)
        {
            var rgx = new Regex("_D\\d{3,4}$");
            string result = param;
            while (rgx.IsMatch(result))
            {
                result = rgx.Split(result)?[0]?? param;
            }
            return result;
        }
        /// <summary>
        /// 正则替换测试
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public string RegexReplaceUnit(string param)
        {
            var rgx = new Regex("_D\\d{3,4}$");
            string result = param;
            while (rgx.IsMatch(result))
            {
                result = rgx.Replace(result, "");
            }
            return result;
        }
    }
}
