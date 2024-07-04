using SofreDaar.Models.Base;
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
    public ICollection<OrderItem> OrderItems { get; set; }
    public ReserveStatus ReserveStatus { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime ReserveDateTime { get; set; }
    public DateTime CancelDateTime { get; set; }
    public bool IsPaymentInCash { get; set; }
    public int PaymentValue { get; set; }
    public string OrderComment { get; set; }
}