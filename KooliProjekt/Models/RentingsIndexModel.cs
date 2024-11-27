using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class RentingsIndexModel
    {
        public RentingsSearch Search {  get; set; }
        
        public PagedResult<Renting> Data { get; set; }
    }
}
