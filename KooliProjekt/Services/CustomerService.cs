
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerService _customerService;

        public CustomerService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<PagedResult<Customer>> List(int page, int pageSize)
        {
            return await _customerService.List(page, 5);
        }

        public async Task<IList<Customer>> Lookup()
        {
            return await _customerService.Lookup();
        }

        public async Task<Customer> Get(int id)
        {
            return await _customerService.Get(id);
        }

        public async Task Save(Customer list)
        {

            await _customerService.Save(list);
        }

        public async Task Delete(int id)
        {
            await _customerService.Delete(id);
        }
    }
}