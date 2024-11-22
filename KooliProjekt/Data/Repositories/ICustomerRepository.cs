namespace KooliProjekt.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Get(int id);
        Task<PagedResult<Customer>> List(int page, int pageSize);
        Task Save(Customer item);
        Task Delete(int id);
    }
}