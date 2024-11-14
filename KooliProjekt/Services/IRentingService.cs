using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IRentingService
    {
        Task<PagedResult<Renting>> List(int page, int pageSize);
        Task<Renting> Get(int id);
        Task Save(Renting list);
        Task Delete(int id);
    }
}
