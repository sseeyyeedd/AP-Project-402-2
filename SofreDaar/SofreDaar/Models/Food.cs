using SofreDaar.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Food:Base.Entity
{
    public string Name { get; set; }
    public Restaurant Restaurant { get; set; }
    [Required]
    public Guid RestaurantId { get; set; }
    [Required]
    public FoodType FoodType { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Commnet> Commnets { get; set; }
}