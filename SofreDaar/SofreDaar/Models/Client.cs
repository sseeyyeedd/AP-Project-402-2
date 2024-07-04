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
    public int ReservesLeft { get; set; }
    [MaxLength(127)]
    public string? SureName { get; set; }
    public string? Email { get; set; }
    [MaxLength(6)]
    public string? VerificationCode { get; set; }
    [MaxLength(11)]
    public string? PhoneNumber { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Commnet> Commnets { get; set; }
    public ICollection<Report> Reports { get; set; }
    public ICollection<Rating> Ratings { get; set; }

    public bool Activate(string code)
    {
        if (code == VerificationCode)
        {
            VerificationCode = "0";
            return true;
        }
        return false;
    }
}