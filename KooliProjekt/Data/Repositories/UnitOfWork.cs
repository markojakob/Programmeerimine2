
namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            ICustomerRepository todoItemRepository,
            ICarRepository carRepository)
        {
            _context = context;

            CustomerRepository = todoItemRepository;
            CarRepository = carRepository;
        }

        public ICustomerRepository CustomerRepository { get; private set; }
        public ICarRepository CarRepository { get; private set; }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
