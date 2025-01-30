
using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<Customer>> List(int page, int pageSize)
        {
            return await _uow.CustomerRepository.List(page, 5);
        }

        public async Task<IList<Customer>> Lookup()
        {
            return await _uow.CustomerRepository.Lookup();
        }

        public async Task<Customer> Get(int id)
        {
            return await _uow.CustomerRepository.Get(id);
        }

        public async Task Save(Customer list)
        {

            await _uow.CustomerRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            await _uow.CustomerRepository.Delete(id);
        }
    }
}