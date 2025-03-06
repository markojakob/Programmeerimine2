
using System.Net.Http;
using System.Net.Http.Json;

namespace WpfApp1.Api
{
    
    class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient() 
        { 
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");

        }

        public async Task<List<Car>> List()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Car>>("Cars");

            return result;
        }

        public async Task Save(Car list)
        {
            if (list.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Cars", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("Cars/" + list.Id, list);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Cars/" + id);
        }
    }
}
