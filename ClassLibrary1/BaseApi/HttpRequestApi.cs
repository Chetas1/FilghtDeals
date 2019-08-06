using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.BaseApi
{
    class HttpRequestApi
    {
        
       
        public async static Task<T> GetDataAsync<T>(string requestApi,string ApiKey)
        {
            var result = new List<T>();
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");
                httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestApi);
                if (httpResponse.IsSuccessStatusCode)
                {   
                    var data = await httpResponse.Content.ReadAsAsync<T>();
                    result.Add(data);
                }

            }
            return result.First();
        }
    }
}
