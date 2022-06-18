namespace FoodShop.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        Task<ServiceResponse<Category>> GetCategoryAsync(int id);
        Task<ServiceResponse<Category>> createCategoryAsync(CategoryCreateVM Category);
        Task<ServiceResponse<Category>> updateCategoryAsync(CategoryUpdateVM Category);
        
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
