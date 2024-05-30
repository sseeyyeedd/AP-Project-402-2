using SofreDaar.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Food:Base.Entity
{
    public Restaurant Restaurant { get; set; }
    public Guid RestaurantId { get; set; }
    [Required]
    public FoodType FoodType { get; set; }
    public ICollection<Order> Orders { get; set; }
}