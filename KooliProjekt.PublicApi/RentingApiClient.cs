using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{

    public class RentingApiClient : IRentingApiClient
    {
        private readonly HttpClient _httpClient;
        public RentingApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/rentings");

        }


        public async Task<Result<List<Renting>>> List()
        {
            var result = new Result<List<Renting>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Renting>>("rentings");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;

        }

        public async Task<Result<Renting>> Get(int id)
        {
            var result = new Result<Renting>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<Renting>($"rentings/{id}");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        public async Task<Result> Save(Renting list)
        {
            var result = new Result();

            try
            {
                if (list.Id == 0)
                {
                    await _httpClient.PostAsJsonAsync("rentings", list);
                }
                else
                {
                    await _httpClient.PutAsJsonAsync("rentings/" + list.Id, list);
                }
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                await _httpClient.DeleteAsync("rentings/" + id);
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }
    }
}