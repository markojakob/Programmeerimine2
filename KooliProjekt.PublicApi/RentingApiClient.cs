
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
            _httpClient.BaseAddress = new Uri("https://localhost:7136/rentingapi/");

        }


        public async Task<Result<List<Renting>>> List()
        {
            var result = new Result<List<Renting>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Renting>>("Rentings");
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
                result.Value = await _httpClient.GetFromJsonAsync<Renting>("Rentings");
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
                   await _httpClient.PostAsJsonAsync("Rentings", list);
                }
                else
                {
                   await _httpClient.PutAsJsonAsync("Rentings/" + list.Id, list);
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
                await _httpClient.DeleteAsync("Rentings/" + id);
            }
            catch(Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        
    }
}
