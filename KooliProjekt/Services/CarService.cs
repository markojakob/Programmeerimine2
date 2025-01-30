using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _uow;

        public CarService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<Car>> List(int page, int pageSize)
        {
            return await _uow.CarRepository.List(page, 5);
        }

        public async Task<Car> Get(int id)
        {
            return await _uow.CarRepository.Get(id);
        }

        public async Task Save(Car list)
        {

            await _uow.CarRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            await _uow.CarRepository.Delete(id);
        }
    }
}