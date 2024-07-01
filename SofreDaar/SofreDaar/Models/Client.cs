using SofreDaar.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Client:Base.User
{
    public Gender Gender { get; set; }
    public string? PostalAddress { get; set; }
    [Required]
    public ClinetSubscription Subscription { get; set; }
    public DateTime SubscriptionStart { get; set; }
    public string PaymentCode { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Commnet> Commnets { get; set; }
    public ICollection<Report> Reports { get; set; }
    public ICollection<Rating> Ratings { get; set; }
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