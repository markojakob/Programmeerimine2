using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface ICarService
    {
        Task<PagedResult<Car>> List(int page, int pageSize, CarsIndexModel query = null);
        Task<Car> Get(int id);
        Task Save(Car list);
        Task Delete(int id);
    }
}
