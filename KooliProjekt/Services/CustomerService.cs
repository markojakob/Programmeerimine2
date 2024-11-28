using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Customers
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Customer> Get(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<PagedResult<Customer>> List(int page, int pageSize, TodoListsSearch search = null)
        {
            var query = _context.Customers.AsQueryable();

            search = search ?? new CustomersSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Title.Contains(search.Keyword));
            }

            if (search.Done != null)
            {
                query = query.Where(list => list.Items.Any());

                if (search.Done.Value)
                {
                    query = query.Where(list => list.Items.All(item => item.IsDone));
                }
                else
                {
                    query = query.Where(list => list.Items.Any(item => !item.IsDone));
                }
            }

            return await query
                .OrderBy(list => list.Title)
                .GetPagedAsync(page, pageSize);
        }
        public async Task Save(Customer list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}