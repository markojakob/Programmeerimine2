using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override async Task<Car> Get(int id)
        {
            return await DbContext.Cars
                .Include(list => list.Rentings)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
