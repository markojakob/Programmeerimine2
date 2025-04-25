using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingApi
{
    public interface IApiClient
    {
        Task<Result<List<Renting>>> List();
        Task<Result> Save(Renting list);
        Task<Result> Delete(int id);

        Task<Result<Renting>> Get(int id);
    }
}
