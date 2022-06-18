using System.ComponentModel.DataAnnotations;

namespace FoodShop.Client.ViewModel
{
    public class CategoryCreateVMC
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Icon is required")]
        public string Icon { get; set; } = string.Empty;
    }
}
