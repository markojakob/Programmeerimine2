using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Cars
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Car> Get(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<PagedResult<Car>> List(int page, int pageSize, CarsSearch search = null)
        {
            var query = _context.Cars.AsQueryable();

            search = search ?? new CarsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.CarMaker.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.CarMaker)
                .GetPagedAsync(page, pageSize);
        }

        public async Task Save(Car list)
        {
            if (list.Id == 0)
            {
                _context.Cars.Add(list);
            }
            else
            {
                _context.Cars.Update(list);
            }

            await _context.SaveChangesAsync();
        }
    }
}
