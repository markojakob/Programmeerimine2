namespace WpfApp1.Api
{
    interface IApiClient
    {
        Task<Result<List<Car>>> List();
        Task Save(Car list);
        Task Delete(int id);
    }
}
