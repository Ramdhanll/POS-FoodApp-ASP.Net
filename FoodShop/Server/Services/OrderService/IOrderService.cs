namespace FoodShop.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<OrderResponse>>> GetOrdersAsync(string status = null);
        Task<ServiceResponse<Order>> GetOrderAsync(string id);
        Task<ServiceResponse<Order>> CreateOrderAsync();
        Task<ServiceResponse<Order>> UpdateOrderItemsAsync(OrderUpdateVM request);
        Task<ServiceResponse<bool>> DeleteOrderAsync(string id);
        
    }
}
