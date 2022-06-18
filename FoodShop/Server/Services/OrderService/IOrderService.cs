namespace FoodShop.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetOrdersAsync();
        Task<ServiceResponse<Order>> GetOrderAsync(string id);
        Task<ServiceResponse<Order>> CreateOrderAsync();
        Task<ServiceResponse<Order>> UpdateOrderAsync(OrderUpdateVM request);
        Task<ServiceResponse<bool>> DeleteOrderAsync(string id);
        
    }
}
