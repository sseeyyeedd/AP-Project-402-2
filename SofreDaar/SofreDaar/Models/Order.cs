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
    public string PaymentCode { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ReserveStatus ReserveStatus { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime ReserveDateTime { get; set; }
    public bool IsPaymentInCash { get; set; }
    public int PaymentValue { get; set; }
    public bool CompletePayment(string code)
    {
        if (code == PaymentCode)
        {
            PaymentCode = "0";
            return true;
        }
        return false;
    }
}