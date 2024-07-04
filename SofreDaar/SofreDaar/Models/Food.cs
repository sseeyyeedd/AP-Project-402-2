using SofreDaar.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Security.RightsManagement;

namespace SofreDaar.Models;

public class Food:Base.Entity
{
    public string Name { get; set; }
    public string RawMaterials { get; set; }
    public string ImageAddress { get; set; }
    public Restaurant Restaurant { get; set; }
    [Required]
    public Guid RestaurantId { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderItem> Orders { get; set; }
    public ICollection<Commnet> Commnets { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}