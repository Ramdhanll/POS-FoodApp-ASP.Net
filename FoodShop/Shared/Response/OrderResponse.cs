using FoodShop.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Shared.Response
{
    public class OrderResponse
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItems> OrderItems { get; set; }
        public Decimal TotalPrice { get; set; } = 0;
        public string OrderStatus { get; set; } = "Ongoing";
    }
}
