using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Template.Remote.HttpApp
{
    /// <summary>
    /// http请求接口
    /// </summary>
    public interface IHttpClientProxy
    {
        Task<T> Request<T>(string url, object param = default, HttpMethod method = default, IDictionary<string, string> header = default);
    }
}
