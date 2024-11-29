using KooliProjekt.Data;
using KooliProjekt.Search;


namespace KooliProjekt.Services
{
    public interface ICustomerService
    {
        Task<PagedResult<Customer>> List(int page, int pageSize, CustomersSearch search = null);
        Task<IList<Customer>> Lookup();
        Task<Customer> Get(int id);
        Task Save(Customer list);
        Task Delete(int id);
    }
}