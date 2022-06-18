namespace FoodShop.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<Category>> CreateCategoryAsync(MultipartFormDataContent content)
        {
            var response = await _http.PostAsync($"/api/categories", content);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Category>>();
            return result;
        }
        
        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var result = await _http.DeleteAsync($"/api/categories/{id}");
            var deleteCategory = (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
            return deleteCategory;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Categories");
            return result;
        }

        public async Task<ServiceResponse<Category>> GetCategoryAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Category>>($"/api/categories/{id}");
            return result;
        }

        public async Task<ServiceResponse<Category>> UpdateCategoryAsync(MultipartFormDataContent content)
        {
            var response = await _http.PutAsync($"/api/categories", content);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Category>>();
            return result;
        }
    }
}
