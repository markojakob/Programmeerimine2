using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{
    public interface IApiClient
    {
        Task<Result<List<Car>>> List();
        Task<Result> Save(Car list);
        Task<Result> Delete(int id);
    }
}
