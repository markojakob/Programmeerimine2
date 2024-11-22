using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Customer> Get(int id)
        {
            return await DbContext.Customers
                .Include(list => list.Cars)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<PagedResult<Customer>> List(int page, int pageSize)
        {
            return await DbContext.Customers
                .GetPagedAsync(page, pageSize);
        }
    }
}
