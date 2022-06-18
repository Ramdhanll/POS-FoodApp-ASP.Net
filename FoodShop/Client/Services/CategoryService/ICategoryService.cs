namespace FoodShop.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        Task<ServiceResponse<Category>> GetCategoryAsync(int id);
        Task<ServiceResponse<Category>> CreateCategoryAsync(MultipartFormDataContent content);
        Task<ServiceResponse<Category>> UpdateCategoryAsync(MultipartFormDataContent content);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);

    }
}
