namespace KooliProjekt.Data.Repositories
{
    public interface ICarRepository
    {
        Task<Car> Get(int id);
        Task<PagedResult<Car>> List(int page, int pageSize);
        Task Save(Car car);
        Task Delete(int id);
    }
}
