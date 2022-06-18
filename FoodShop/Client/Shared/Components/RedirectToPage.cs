using Microsoft.AspNetCore.Components;

namespace FoodShop.Client.Shared.Components
{
    public class RedirectToPage: ComponentBase
    {
        [Inject]
        private NavigationManager Navigation { get; set; }

        [Parameter]
        public string Href { get; set; }

        protected override void OnInitialized()
            => Navigation.NavigateTo(Href);
    }
}
