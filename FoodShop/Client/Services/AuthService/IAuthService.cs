namespace FoodShop.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLoginVM request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordVM request);
        Task<bool> IsUserAuthenticated();
    }
}
