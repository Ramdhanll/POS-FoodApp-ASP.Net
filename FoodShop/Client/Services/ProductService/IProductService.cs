namespace FoodShop.Client.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int id);
        Task<ServiceResponse<Product>> CreateProductAsync(MultipartFormDataContent content);
        Task<ServiceResponse<Product>> UpdateProductAsync(MultipartFormDataContent content);
        
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
