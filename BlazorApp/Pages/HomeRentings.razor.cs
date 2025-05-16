using KooliProjekt.PublicApi;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KooliProjekt.BlazorApp.Pages
{
    public partial class HomeRentings : ComponentBase
    {
        [Inject]
        protected IRentingApiClient rentingApiClient { get; set; }

        [Inject]
        protected ICustomerApiClient customerApiClient { get; set; }

        [Inject]
        protected IApiClient carApiClient { get; set; }

        [Inject]
        protected IJSRuntime RentingJsRuntime { get; set; }

        [Inject]
        protected NavigationManager RentingNavManager { get; set; }

        protected List<Renting> rentings { get; set; }
        protected List<Car> cars { get; set; }
        protected List<Customer> customers { get; set; }

        protected override async Task OnInitializedAsync()
        {

            var result = await rentingApiClient.List();
            rentings = result.Value;
            var carResult = await carApiClient.List();
            cars = carResult.Value;
            var customerResult = await customerApiClient.List();
            customers = customerResult.Value;

        }
        protected string GetCustomerName(int customerId)
        {
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            return customer?.FullName ?? "Unknown Customer";
        }
        protected string GetCarDisplayName(int carId)
        {
            var car = cars.FirstOrDefault(c => c.Id == carId);
            return car != null ? $"{car.CarMaker} {car.Model}" : "Unknown Car";
        }
        protected async Task Delete(int id)
        {
            bool confirmed = await RentingJsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed)
            {
                return;
            }

            await rentingApiClient.Delete(id);

            RentingNavManager.Refresh();
        }
    }
}