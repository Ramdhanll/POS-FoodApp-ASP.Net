namespace FoodShop.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int id);
        Task<ServiceResponse<Product>> CreateProductAsync(ProductCreateVM product);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
        Task<ServiceResponse<Product>> UpdateProductAsync(ProductUpdateVM product);
    }
}
