using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Commnet:Base.Entity
{
    public string Text { get; set; }
    public Food Food { get; set; }
    [Required]
    public Guid FoodId { get; set; }
    public Client Client { get; set; }
    [Required]
    public Guid ClientId { get; set; }
}