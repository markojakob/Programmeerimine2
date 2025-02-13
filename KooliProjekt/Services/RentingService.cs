using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class RentingService : IRentingService
    {
        private readonly IUnitOfWork _uow;

        public RentingService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<Renting>> List(int page, int pageSize)
        {
            return await _uow.RentingRepository.List(page, 5);
        }

        public async Task<Renting> Get(int id)
        {
            return await _uow.RentingRepository.Get(id);
        }

        public async Task Save(Renting list)
        {
            await _uow.RentingRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            await _uow.RentingRepository.Delete(id);
        }
    }
}
