using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class RentingService : IRentingService
    {
        private readonly IRentingService _rentingService;

        public RentingService(IRentingService rentingService)
        {
            _rentingService = rentingService;
        }

        public async Task<PagedResult<Renting>> List(int page, int pageSize)
        {
            return await _rentingService.List(page, 5);
        }

        public async Task<Renting> Get(int id)
        {
            return await _rentingService.Get(id);
        }

        public async Task Save(Renting list)
        {
            await _rentingService.Save(list);
        }

        public async Task Delete(int id)
        {
            await _rentingService.Delete(id);
        }
    }
}
