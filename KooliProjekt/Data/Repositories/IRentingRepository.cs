namespace KooliProjekt.Data.Repositories
{
    public interface IRentingRepository
    {
        Task<Renting> Get(int id);
        Task<PagedResult<Renting>> List(int page, int pageSize);
        Task Save(Renting item);
        Task Delete(int id);
    }
}
