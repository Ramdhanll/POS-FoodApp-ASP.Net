using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodShop.Shared.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        public List<Category> Categories { get; set; }

        [JsonIgnore]
        public List<OrderItems> OrderItems { get; set; }
    }
}
