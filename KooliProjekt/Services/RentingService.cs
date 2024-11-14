using KooliProjekt.Data;
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

        public async Task<PagedResult<Renting>> List(int page, int pageSize)
        {
            return await _context.Rentings.GetPagedAsync(page, 5);
        }

        public async Task<Renting> Get(int id)
        {
            return await _context.Rentings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Renting list)
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

        public async Task Delete(int id)
        {
            var renting = await _context.Rentings.FindAsync(id);
            if (renting != null)
            {
                _context.Rentings.Remove(renting);
                await _context.SaveChangesAsync();
            }
        }
    }
}
