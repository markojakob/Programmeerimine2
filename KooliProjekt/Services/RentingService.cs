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
            var query = _context.Rentings.AsQueryable();

            search = search ?? new RentingsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            { 
                query = query.Where(list => list.RentalNo.ToString().Contains(search.Keyword));
            }

            // Done asemel IsActive vmt
            // true = rent on käimas
            // false = rent on lõppenud või pole alanud

            if (search.Active != null)
            {
                // Kas on aktivne või mitte - seda saab otsustada RentalDate ja
                // RentalDueTime abil
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
