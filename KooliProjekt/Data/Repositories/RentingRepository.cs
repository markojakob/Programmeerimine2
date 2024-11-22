using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class RentingRepository : BaseRepository<Renting>, IRentingRepository
    {
        public RentingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Renting> Get(int id)
        {
            return await DbContext.Rentings
                .Include(list => list.Lines)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<PagedResult<Renting>> List(int page, int pageSize)
        {
            return await DbContext.Rentings
                .GetPagedAsync(page, pageSize);
        }
    }
}
