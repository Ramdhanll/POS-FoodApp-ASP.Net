namespace FoodShop.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<Order>> CreateOrderAsync()
        {
            var order = new Order();
            
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            return new ServiceResponse<Order>
            {
                Success = true,
                Data = order,
                Message = "Order has been created"
            };
        }

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Order not found!"
                };
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Order has been deleted"
            };
        }

        public Task<ServiceResponse<Order>> GetOrderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Order>>> GetOrdersAsync()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();

            return new ServiceResponse<List<Order>>
            {
                Success = true,
                Data = orders,
                Message = "Orders retrieved"
            };

        }

        public async Task<ServiceResponse<Order>> UpdateOrderAsync(OrderUpdateVM request)
        {
            // find order by id
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == request.Id);
            if (order == null)
            {
                return new ServiceResponse<Order>
                {
                    Success = false,
                    Message = "Order not found!"
                };
            }

            // add or update orderItem
            foreach (var item in request.OrderItems)
            {
                if (order.OrderItems.Any(o => o.Product.Id == item.ProductId))
                {
                    var orderItem = order.OrderItems.FirstOrDefault(o => o.Product.Id == item.ProductId);
                    orderItem.Qty = item.Qty;
                }
                else
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        return new ServiceResponse<Order>
                        {
                            Success = false,
                            Message = "Product not found!"
                        };
                    }
                    
                    var orderItem = new OrderItems
                    {
                        Order = order,
                        Product = product,
                        Qty = item.Qty
                    };
                    
                    order.OrderItems.Add(orderItem);
                }
            }

            // get total price
            order.TotalPrice = order.OrderItems.Sum(oi => oi.Product.Price * oi.Qty);

            await _context.SaveChangesAsync();

            return new ServiceResponse<Order>
            {
                Success = true,
                Data = order,
                Message = "Order has been updated"
            };
        }
    }
}
