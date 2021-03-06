using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Shared.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<OrderItems> OrderItems { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal TotalPrice { get; set; } = 0;
        public EnumOrderStatus OrderStatus { get; set; } = EnumOrderStatus.Ongoing;
    }

    public enum EnumOrderStatus
    {
        [Display(Name = "Ongoing")]
        Ongoing,
        [Display(Name = "Cancelled")]
        Cancelled,
        [Display(Name = "Completed")]
        Completed
    }
}
