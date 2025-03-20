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


        public async Task<Result<List<Car>>> List()
        {
            var result = new Result<List<Car>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Car>>("Cars");
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;

        }

        public async Task<Result> Save(Car list)
        {
            var result = new Result();

            try
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
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                await _httpClient.DeleteAsync("Cars/" + id);
            }
            catch(Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
    }
}
