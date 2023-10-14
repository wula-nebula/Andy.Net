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
    [NonController]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private IServiceProvider serviceProvider;
        public TestController(IServiceProvider ServiceProvider)
        {
            serviceProvider = ServiceProvider;
        }
        /// <summary>
        /// AutoMapper测试
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public UserTarget AutoConvert(UserSource request)
        {
            return AutoMapperExt<UserSource, UserTarget>.AutoConvert(request);
        }
        /// <summary>
        /// IHttpClientFactory测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> uuid()
        {
            var result = await HttpHelper.Request<string>("https://www.baidu.com/", default, HttpMethod.Get);
            return result;
        }
        /// <summary>
        /// IHttpClientFactory测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> uuid1()
        {
            var result = await HttpHelper.Request<string>("https://www.toutiao.com", default, HttpMethod.Get);
            return result;
        }
        /// <summary>
        /// 乱码测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> testcode()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("api", "/dis/api/credit_control/balance_alarm/get_customer_balance");
            headers.Add("appId", "1579757937714364416");
            headers.Add("sign", "8d98ba3edd3226e34328d027cde60412");
            headers.Add("caller", "dp-dis");
            headers.Add("version", "1.0");
            headers.Add("time", "1678167816");
            headers.Add("requestId", "70928956");
            var result = await HttpHelper.Request<dynamic>("http://uat-dmsc-api.eminxing.com/dis/api/credit_control/balance_alarm/get_customer_balance?current_page=1&page_number=30000&balance_date=2023-03-07", default, HttpMethod.Get, headers);
            return result;
        }

        /// <summary>
        /// 乱码测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> testoms()
        {
            var request = new
            {
                data = new
                {
                    hawbcode = "YT2135122078000013",
                    server_channel_code = "TVC002",
                    SystemCode = "1001",
                    plc_code = 0,
                    operation_id = 0
                }
            };
            var result = await HttpHelper.Request<dynamic>("http://192.168.122.219:5000/business/api/UpdateOrderChannel", request, HttpMethod.Post);
            return result;
        }
        /// <summary>
        /// 获取utc时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        [HttpGet]
        public DateTime GetUtcTime(long ticks)
        {
            return DateTimeHelper.GetUtcDateTime(ticks);
        }


        /// <summary>
        /// 测试依赖注入
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetLife(string order_id)
        {
            Task.Run(()=>TestLife(order_id));
            return "";
        }
        [NonAction]
        public string TestLife(string order_id)
        {
            for (int i = 0; i < 2000; i++)
            {

            }
            var testServ = serviceProvider.GetRequiredService<ITestService>();
            return testServ.Get(order_id);
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
