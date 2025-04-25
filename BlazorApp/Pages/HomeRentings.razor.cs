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
        protected IJSRuntime RentingJsRuntime { get; set; }

        [Inject]
        protected NavigationManager RentingNavManager { get; set; }

        protected List<Renting> rentings { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await rentingApiClient.List();
            rentings = result.Value;
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