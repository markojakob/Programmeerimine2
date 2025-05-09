using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi
{
	public interface IRentingApiClient
	{

		Task<Result<List<Renting>>> List();

		Task<Result> Save(Renting list);


		Task<Result> Delete(int id);


		Task<Result<Renting>> Get(int id);

	}
}
