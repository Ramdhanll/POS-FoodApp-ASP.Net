using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Shared.VirtualModel
{
    public enum GetOrdersVM
    {
        [Display(Name = "Ongoing")]
        Ongoing,
        [Display(Name = "Cancelled")]
        Cancelled,
        [Display(Name = "Completed")]
        Completed,
        [Display(Name = "All")]
        All
    }
}
