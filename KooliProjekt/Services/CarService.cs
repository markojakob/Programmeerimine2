using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<PagedResult<Car>> List(int page, int pageSize)
        {
            return await _carRepository.List(page, 5);
        }

        public async Task<Car> Get(int id)
        {
            return await _carRepository.Get(id);
        }

        public async Task Save(Car list)
        {

            await _carRepository.Save(list);
        }

        public async Task Delete(int id)
        {
            await _carRepository.Delete(id);
        }
    }
}