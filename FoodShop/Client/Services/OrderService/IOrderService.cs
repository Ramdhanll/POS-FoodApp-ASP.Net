namespace FoodShop.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<OrderResponse>>> GetOrdersAsync();
        Task<ServiceResponse<Order>> GetOrderAsync(int id);
        Task<ServiceResponse<Order>> CreateOrderAsync();
        Task<ServiceResponse<Order>> UpdateOrderAsync(MultipartFormDataContent content);
        
        Task<ServiceResponse<bool>> DeleteOrderAsync(string id);
    }
}
