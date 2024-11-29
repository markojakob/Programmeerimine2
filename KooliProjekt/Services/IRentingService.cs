using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IRentingService
    {
        Task<PagedResult<Renting>> List(int page, int pageSize, RentingsSearch query = null);
        Task<Renting> Get(int id);
        Task Save(Renting list);
        Task Delete(int id);
    }
}
