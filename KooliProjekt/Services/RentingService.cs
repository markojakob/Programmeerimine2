using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class RentingService : IRentingService
    {
        private readonly ApplicationDbContext _context;

        public RentingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var rentingToDelete = await _context.Rentings.FindAsync(id);


            if (rentingToDelete != null)
            {
                _context.Rentings.Remove(rentingToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Renting> Get(int id)
        {
            return await _context.Rentings.FindAsync(id);
        }

        public async Task<PagedResult<Renting>> List(int page, int pageSize, RentingsSearch search = null)
        {
            // Start with a queryable collection of all rentals from the database context.
            var query = _context.Rentings.AsQueryable();

            // If no search criteria are provided, create a default empty search object.
            search = search ?? new RentingsSearch();
            
            // Apply the Keyword filter if provided in the search criteria.
            // Filters rentals where the RentalNo (converted to string) contains the search Keyword.
            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.RentalNo.ToString().Contains(search.Keyword));
            }

            // Filter by the Active status if specified in the search criteria.
            if (search.Active != null)
            {
                
                var now = DateTime.Now;

                if (search.Active == true)
                {
                    
                    query = query.Where(list =>
                        list.RentalDate >= now && list.RentalDueTime > now
                    );
                }
                else if (search.Active == false)
                {
                    query = query.Where(list =>
                        !(list.RentalDate >= now && list.RentalDueTime > now)
                    );
                }
            }

            // Apply eager loading for the related Customer entity to avoid lazy loading issues.
            // Orders the rentals by their RentalNo in ascending order for predictable pagination.
            return await query
                .Include(renting => renting.Customer) 
                .OrderBy(list => list.RentalNo)      
                .GetPagedAsync(page, pageSize);      
        }

        public async Task Save(Renting list)
        {
            if (list.Id == 0)
            {
                _context.Rentings.Add(list);
            }
            else
            {
                _context.Rentings.Update(list);
            }

            await _context.SaveChangesAsync();
        }
    }
}
