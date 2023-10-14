using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Template.Remote
{
    public class HttpTest
    {
        public static async Task<string> Get(string url)
        {
            try
            {
                var socketsHandler = new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                };
                //using HttpClient http = new HttpClient();
                using (HttpClient http = new HttpClient(socketsHandler))
                {
                    HttpResponseMessage response = await http.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }
        public static async Task<string> GetUS(string url)
        {
            try
            {
                using HttpClient http = new HttpClient();
                HttpResponseMessage response = await http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public static async Task<byte[]> GetFile(string url)
        {
            try
            {
                using HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 5.0; Windows NT; DigExt)");
                HttpResponseMessage response = await http.SendAsync(new HttpRequestMessage { RequestUri = new Uri(url) });
                response.EnsureSuccessStatusCode();
                var remoteBytes = await response.Content.ReadAsByteArrayAsync();
                return remoteBytes;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }
    }
}
