using System.IO;

namespace FoodShop.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<ServiceResponse<Product>> CreateProductAsync(MultipartFormDataContent content)
        {
            var result = await _http.PostAsync("api/products", content);
            var newProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>());
            return newProduct;
        }
        
        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var result = await _http.DeleteAsync($"/api/products/{id}");
            var deleteProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
            return deleteProduct;
        }


        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"/api/products/{id}");
            return result;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products");
            return result;
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(MultipartFormDataContent content)
        {
            var response = await _http.PutAsync($"/api/products", content);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
            return result;
        }
    }
}
