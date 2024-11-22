namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

        ICarRepository CarRepository { get; }
        IRentingRepository RentingRepository { get; }
        ICustomerRepository CustomerRepository { get; }

    }
}
