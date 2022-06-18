using Microsoft.AspNetCore.Components;

namespace FoodShop.Client.Services
{
    public partial class FileService
    {
        private readonly NavigationManager navigationManager;

        public FileService(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
    }
}
