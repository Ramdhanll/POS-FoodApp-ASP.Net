using System.ComponentModel.DataAnnotations;

namespace FoodShop.Client.ViewModel
{
    public class ProductUpdateVMC
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; } = String.Empty;

        [Display(Name = "Categories")]
        [Required, MinLength(1)]
        public IEnumerable<string> categorySelected { get; set; } = new HashSet<string>() { };
        
        [Required(ErrorMessage = "Price is required"), Range(1, Int32.MaxValue)]
        public Decimal Price { get; set; }
        
    }
}
