using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface ICarService
    {
        Task<PagedResult<Car>> List(int page, int pageSize, CarsSearch query = null);
        Task<Car> Get(int id);
        Task Save(Car list);
        Task Delete(int id);
    }
}
