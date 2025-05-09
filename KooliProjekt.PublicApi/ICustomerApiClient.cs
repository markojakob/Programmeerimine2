using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{
    public interface ICustomerApiClient
    {
        Task<Result<List<Customer>>> List();
        Task<Result> Save(Customer list);
        Task<Result> Delete(int id);

        Task<Result<Customer>> Get(int id);
    }
}
