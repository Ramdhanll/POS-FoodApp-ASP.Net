using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodShop.Shared.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }
    }

}
