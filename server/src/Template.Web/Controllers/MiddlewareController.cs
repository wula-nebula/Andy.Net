using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application;
using Template.Elastic;

namespace Template.Web.Controllers
{
    [NonController]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MiddlewareController : ControllerBase
    {
        private readonly IElasticService _service;

        public MiddlewareController(IElasticService service)
        {
            _service = service;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Add(UserSource request)
        {
            return await _service.Write(new EsBulkRequest { type = EsBulkType.create, index = ConfigIndex.User, id = request.name, data = new object[] { request } });
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Update(UserSource request)
        {
            return await _service.Write(new EsBulkRequest { type = EsBulkType.update, index = ConfigIndex.User, id = request.name, data = new object[] { request } });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> Delete(string name)
        {
            return await _service.Write(new EsBulkRequest { type = EsBulkType.delete, index = ConfigIndex.User, id = name });
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> Get(string id)
        {
            var result = await _service.Ready<UserSource>(ConfigIndex.User, id);
            return result;
        }
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetList()
        {
            var condition = new
            {
                from = 0,
                size = 10,
                query = new
                {
                    @bool = new
                    {
                        must = new object[] { new { match_all = new { } } }
                    }
                }
            };
            return await _service.Ready<UserSource>(new EsSearchRequest { index = ConfigIndex.User, param = condition });
        }
        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> BulkAdd(List<UserSource> request)
        {
            var param = new List<object>();
            foreach (var item in request)
            {
                param.Add(new { index = new { _index = ConfigIndex.User, _id = item.name } });
                param.Add(item);
            }
            var result = await _service.WriteBulk(param);
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetCustomer(string id)
        {
            var result = await _service.Ready<dynamic>(ConfigIndex.Customer, id);
            return result;
        }
    }
}
