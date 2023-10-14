using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Elastic;

namespace Template.Application
{
    public class ElasticService : IElasticService
    {
        private readonly IElasticLowLevelClient _client;
        public ElasticService(IElasticLowLevelClient client)
        {
            _client = client;
        }
        /// <summary>
        /// ES读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> Ready<T>(string index, string id)
        {
            var esRes = await _client.GetAsync<StringResponse>(index, id);
            var esBody = JsonConvert.DeserializeObject<JToken>(esRes.Body);
            if (!esRes.Success)
            {
                if (!esBody["found"].ToObject<bool>()) return default(T);
                else throw esRes.OriginalException;
            };
            return esBody["_source"].ToObject<T>();
        }
        /// <summary>
        /// ES读取-条件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EsSearchResponce> Ready<T>(EsSearchRequest request)
        {
            try
            {
                var result = new EsSearchResponce();
                var esRes = await _client.SearchAsync<StringResponse>(request.index, PostData.Serializable(request.param));
                if (!esRes.Success) throw esRes.OriginalException;
                var esBody = JsonConvert.DeserializeObject<JToken>(esRes.Body);
                result.total = esBody?["hits"]?["total"]?["value"]?.Value<int>() ?? 0;
                result.item = esBody?["hits"]?["hits"]?.Select(t => t["_source"].ToObject<T>());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ES写入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> Write(EsBulkRequest request)
        {
            try
            {
                var result = request.type switch
                {
                    EsBulkType.index => await Index(request),
                    EsBulkType.create => await Create(request),
                    EsBulkType.update => await Update(request),
                    EsBulkType.delete => await Delete(request),
                    _ => throw new Exception("")
                };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ES批量写入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> WriteBulk(List<object> request)
        {
            var result = await _client.BulkAsync<StringResponse>(PostData.MultiJson(request));
            if (!result.Success) throw result.OriginalException;
            return result.Success;
        }

        #region ES基础方法
        /// <summary>
        /// 创建，支持Create，Update
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<bool> Index(EsBulkRequest request)
        {
            StringResponse result;
            if (string.IsNullOrWhiteSpace(request.id))
                result = await _client.IndexAsync<StringResponse>(request.index, request.id, PostData.Serializable(request.data[0]));
            else
                result = await _client.IndexAsync<StringResponse>(request.index, PostData.Serializable(request.data[0]));
            if (!result.Success) throw result.OriginalException;
            return result.Success;
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<bool> Create(EsBulkRequest request)
        {
            var result = await _client.CreateAsync<StringResponse>(request.index, request.id, PostData.Serializable(request.data[0]));
            if (!result.Success) throw result.OriginalException;
            return result.Success;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<bool> Update(EsBulkRequest request)
        {
            var result = await _client.UpdateAsync<StringResponse>(request.index, request.id, PostData.Serializable(new { doc = request.data[0] }));
            if (!result.Success) throw result.OriginalException;
            return result.Success;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<bool> Delete(EsBulkRequest request)
        {
            var result = await _client.DeleteAsync<StringResponse>(request.index, request.id);
            if (!result.Success) throw result.OriginalException;
            return result.Success;
        }
        #endregion
    }
}
