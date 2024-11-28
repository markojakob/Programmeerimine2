using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class CustomersIndexModel
    {
        public CustomersSearch Search { get; set; }

        public PagedResult<Customer> Data { get; set; }
    }
}
