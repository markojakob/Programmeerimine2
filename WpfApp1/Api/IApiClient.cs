using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    interface IApiClient
    {
        Task<List<Car>> List();
        Task Save(Car list);
        Task Delete(int id);
    }
}
