using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{

    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient _httpClient;
        public CustomerApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/customers");

        }


        public async Task<Result<List<Customer>>> List()
        {
            var result = new Result<List<Customer>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Customer>>("Customers");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;

        }

        public async Task<Result<Customer>> Get(int id)
        {
            var result = new Result<Customer>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<Customer>("Customers");
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }

        public async Task<Result> Save(Customer list)
        {
            var result = new Result();

            try
            {
                if (list.Id == 0)
                {
                    await _httpClient.PostAsJsonAsync("Customers", list);
                }
                else
                {
                    await _httpClient.PutAsJsonAsync("Customers/" + list.Id, list);
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
                await _httpClient.DeleteAsync("Customers/" + id);
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }

            return result;
        }


    }
}