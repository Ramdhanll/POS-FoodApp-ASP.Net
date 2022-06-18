using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Shared.VirtualModel
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public List<int> CategoriesId { get; set; }
        public Decimal Price { get; set; }

    }
}
