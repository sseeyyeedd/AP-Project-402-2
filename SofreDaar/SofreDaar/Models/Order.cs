namespace SofreDaar.Models;

public class Order:Base.Entity
{
    public Restaurant Restaurant { get; set; }
    public Guid RestaurantId { get; set; }
    public Client Client { get; set; }
    public Guid ClientId { get; set; }
    public ICollection<Food> Foods { get; set; }
}