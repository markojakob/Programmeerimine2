using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface IRentingService
    {
        Task<PagedResult<Renting>> List(int page, int pageSize, RentingsIndexModel query = null);
        Task<Renting> Get(int id);
        Task Save(Renting list);
        Task Delete(int id);
    }
}
