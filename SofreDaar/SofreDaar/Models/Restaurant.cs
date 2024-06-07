using SofreDaar.Models.Base;
using System.ComponentModel.DataAnnotations;
namespace SofreDaar.Models;

public class Restaurant:Base.User
{
    [MaxLength(63)]
    public string City { get; set; }
    [Required]
    public RestaurantReceptionType ReceptionType { get; set; }
    [MaxLength(255)]
    public string Address { get; set; }
    public ICollection<Food> Foods { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Report> Reports { get; set; }
}