using System.IO;

namespace FoodShop.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<ServiceResponse<Order>> CreateOrderAsync()
        {
            var result = await _http.PostAsync("api/orders", null);
            var newOrder = (await result.Content.ReadFromJsonAsync<ServiceResponse<Order>>());
            return newOrder;
        }
        
        public async Task<ServiceResponse<bool>> DeleteOrderAsync(string id)
        {
            var result = await _http.DeleteAsync($"/api/orders/{id}");
            var deleteOrder = (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
            return deleteOrder;
        }


        public async Task<ServiceResponse<Order>> GetOrderAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Order>>($"/api/orders/{id}");
            return result;
        }

        public async Task<ServiceResponse<List<OrderResponse>>> GetOrdersAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderResponse>>>("api/orders");
            return result;
        }

        public async Task<ServiceResponse<Order>> UpdateOrderAsync(MultipartFormDataContent content)
        {
            var response = await _http.PutAsync($"/api/orders", content);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Order>>();
            return result;
        }
    }
}
