using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Order:Base.Entity
{
    public Restaurant Restaurant { get; set; }
    [Required]
    public Guid RestaurantId { get; set; }
    public Client Client { get; set; }
    [Required]
    public Guid ClientId { get; set; }
    public ICollection<Food> Foods { get; set; }
}