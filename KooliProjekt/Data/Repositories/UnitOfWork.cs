
namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            ICustomerRepository customerRepository,
            ICarRepository carRepository,
            IRentingRepository rentingRepository)
        {
            _context = context;

            CustomerRepository = customerRepository;
            CarRepository = carRepository;
            RentingRepository = rentingRepository;
        }

        public ICustomerRepository CustomerRepository { get; private set; }
        public ICarRepository CarRepository { get; private set; }

        public IRentingRepository RentingRepository { get; private set; }



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
