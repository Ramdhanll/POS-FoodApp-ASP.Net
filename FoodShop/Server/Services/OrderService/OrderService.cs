using FoodShop.Server.Utils;
using System.Linq;

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

        public async Task<ServiceResponse<List<OrderResponse>>> GetOrdersAsync(string status = null)
        {
            if (status != null)
            {
                var statusToLower = status.ToLower();
                var statusFilter = statusToLower.Substring(0, 1).ToUpper() + statusToLower.Substring(1);
                EnumOrderStatus orderStatus = EnumOrderStatus.Ongoing;

                try
                {
                    orderStatus = (EnumOrderStatus)Enum.Parse(typeof(EnumOrderStatus), statusFilter);
                }
                catch (Exception)
                {

                        return new ServiceResponse<List<OrderResponse>>
                        {
                            Success = true,
                            Data = null,
                            Message = "Status not found!"
                        };
                }

                var orders = await _context.Orders
                       .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                       .Where(o => o.OrderStatus == orderStatus)
                       .Select(o => new OrderResponse
                       {
                           Id = o.Id,
                           CreatedAt = o.CreatedAt,
                           OrderItems = o.OrderItems,
                           TotalPrice = o.TotalPrice,
                           OrderStatus = o.OrderStatus.GetDisplayName()
                       })
                       .ToListAsync();

                return new ServiceResponse<List<OrderResponse>>
                {
                    Success = true,
                    Data = orders,
                    Message = "Orders has been found"
                };
            }
            else
            {
                var orders = await _context.Orders
                    .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                    .Select(o => new OrderResponse
                    {
                        Id = o.Id,
                        CreatedAt = o.CreatedAt,
                        OrderItems = o.OrderItems,
                        TotalPrice = o.TotalPrice,
                        OrderStatus = o.OrderStatus.GetDisplayName()
                    })
                    .ToListAsync();

              
                return new ServiceResponse<List<OrderResponse>>
                {
                    Success = true,
                    Data = orders,
                    Message = "Orders has been found"
                };
            }
        }

        public async Task<ServiceResponse<Order>> UpdateOrderItemsAsync(OrderUpdateVM request)
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
