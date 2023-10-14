using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Template.Remote.HttpApp
{
    public class HttpClientProxy : IHttpClientProxy
    {
        private const string HttpClientName = "template-http";
        private const string jsonType = "application/json";
        private readonly HttpClient httpClient;
        public HttpClientProxy(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(HttpClientName);
        }
        public async Task<T> Request<T>(string url, object param, HttpMethod method = null, IDictionary<string, string> header = null)
        {
            try
            {
                if (header != default)
                    foreach (var item in header)
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);

                var resultTask = method switch
                {
                    _ when HttpMethod.Get == method => httpClient.GetAsync(url),
                    _ when HttpMethod.Post == method => PostAsync(httpClient, url, JsonConvert.SerializeObject(param)),
                    _ when HttpMethod.Put == method => PutAsync(httpClient, url, JsonConvert.SerializeObject(param)),
                    _ => throw new Exception($"unsupport httpmethod {method}")
                };
                using HttpResponseMessage result = await resultTask;
                result.EnsureSuccessStatusCode();
                var remoteJson = await result.Content.ReadAsStringAsync();
                if (typeof(T) == typeof(string)) return (T)(object)remoteJson;

                return string.IsNullOrWhiteSpace(remoteJson) ? default : JsonConvert.DeserializeObject<T>(remoteJson);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Post查询
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> PostAsync(HttpClient client, string path, string body)
        {
            HttpContent content = new StringContent(body, Encoding.UTF8);
            var mediatype = new MediaTypeHeaderValue(jsonType);
            mediatype.CharSet = Encoding.UTF8.BodyName;
            content.Headers.ContentType = mediatype;
            return await client.PostAsync(path, content);
        }
        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static async Task<HttpResponseMessage> PutAsync(HttpClient client, string path, string body)
        {
            HttpContent content = new StringContent(body, Encoding.UTF8);
            var mediatype = new MediaTypeHeaderValue(jsonType);
            mediatype.CharSet = Encoding.UTF8.BodyName;
            content.Headers.ContentType = mediatype;
            return await client.PutAsync(path, content);
        }
    }
}
