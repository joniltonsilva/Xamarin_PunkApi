using Newtonsoft.Json;
using Projeto.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ApiService))]
namespace Projeto.Services
{
    public class ApiService : IApiService
    {

        private HttpClient _httpClient;

        public async Task<T> GetAsync<T>(string url, Action<T> callback = null)
        {
            _httpClient = _httpClient ?? new HttpClient();
            var response = await _httpClient.GetStringAsync(new Uri(url));
            return JsonConvert.DeserializeObject<T>(response);
        }

        public void Dispose()
        {

        }
    }
}
