global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
global using FoodShop.Shared.Models;
global using FoodShop.Shared.VirtualModel;
global using FoodShop.Shared.Response;
global using FoodShop.Client.Services.ProductService;
global using FoodShop.Client.Services.CategoryService;
global using FoodShop.Client.Services.AuthService;
global using System.Net.Http.Json;
global using Tewr.Blazor.FileReader;
global using FoodShop.Client.Services;
global using FoodShop.Client.ViewModel;
global using FoodShop.Client.Shared.Category;




using FoodShop.Client;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Modal;
using MudBlazor.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredModal();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<FileService>();

// MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.ShowTransitionDuration = 300;
    config.SnackbarConfiguration.HideTransitionDuration = 300;
    config.SnackbarConfiguration.VisibleStateDuration = 1800;
    config.SnackbarConfiguration.PreventDuplicates = false;
    

});

// upload image
builder.Services.AddFileReaderService();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
