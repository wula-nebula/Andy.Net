using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Template.Remote
{
    public static class HttpHelper
    {
        private const string jsonType = "application/json";
        private static HttpClient client;

        static HttpHelper()
        {
            if (client == null)
            {
                var socketsHandler = new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                    AutomaticDecompression = System.Net.DecompressionMethods.All
                };
                client = new HttpClient(socketsHandler);
                //client.DefaultRequestHeaders.Connection.Add("keep-alive");
            }
        }

        /// <summary>
        /// 请求接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="body">请求参数</param>
        /// <param name="method">请求方式</param>
        /// <param name="header">请求header</param>
        /// <returns></returns>
        public static async Task<T> Request<T>(string url, object body, HttpMethod method = null, IDictionary<string, string> header = null)
        {
            try
            {
                if (header != default)
                {
                    if (client.DefaultRequestHeaders.Any() == true)
                    {
                        client.DefaultRequestHeaders.Clear();
                        //client.DefaultRequestHeaders.Connection.Add("keep-alive");
                    }

                    foreach (var item in header)
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                var resultTask = method switch
                {
                    _ when HttpMethod.Get == method => client.GetAsync(url),
                    _ when HttpMethod.Post == method => PostAsync(url, JsonConvert.SerializeObject(body)),
                    _ when HttpMethod.Put == method => PutAsync(url, JsonConvert.SerializeObject(body)),
                    _ => throw new Exception($"unsupport httpmethod {method}")
                };
                using HttpResponseMessage result = await resultTask;
                result.EnsureSuccessStatusCode();
                var remoteJson = await result.Content.ReadAsStringAsync();

                bool IsGzip = result.Content.Headers.ContentEncoding.Contains("gzip");

                if (IsGzip)
                {
                    Stream stm = result.Content.ReadAsStreamAsync().Result;
                    string strHTML = string.Empty;
                    GZipStream gzip = new GZipStream(stm, CompressionMode.Decompress);//解压缩
                    using (StreamReader reader = new StreamReader(gzip, Encoding.UTF8))
                    {
                        strHTML = reader.ReadToEnd();
                    }
                }
                else
                {
                    var outputStr = result.Content.ReadAsStringAsync().Result;
                }
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
        private static async Task<HttpResponseMessage> PostAsync(string path, string body)
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
        private static async Task<HttpResponseMessage> PutAsync(string path, string body)
        {
            HttpContent content = new StringContent(body, Encoding.UTF8);
            var mediatype = new MediaTypeHeaderValue(jsonType);
            mediatype.CharSet = Encoding.UTF8.BodyName;
            content.Headers.ContentType = mediatype;
            return await client.PutAsync(path, content);
        }
    }
}
