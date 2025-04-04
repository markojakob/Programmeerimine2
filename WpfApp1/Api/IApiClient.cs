namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<Result<List<Car>>> List();
        Task<Result> Save(Car list);
        Task<Result> Delete(int id);
    }
}
