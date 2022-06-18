using System.ComponentModel.DataAnnotations;

namespace FoodShop.Client.ViewModel
{
    public class CategoryUpdateVMC
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Icon is required")]
        public string Icon { get; set; }
        public bool IsIconUpdate { get; set; }
    }
}
