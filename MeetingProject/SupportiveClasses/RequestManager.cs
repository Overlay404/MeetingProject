using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MeetingProject.SupportiveClasses
{
    internal class RequestManager
    {
        public static string URL = "https://api.github.com/";
        public static HttpClient httpClient = new HttpClient();

        public static async Task<T> Get<T>(string controller)
        {
            HttpResponseMessage response = null;
            T data = default;
            try
            {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MeetingProject", "1.0"));
                var request = new HttpRequestMessage(HttpMethod.Get, URL + controller);
                response = await http.SendAsync(request);
                data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return data;
        }
    }
}
