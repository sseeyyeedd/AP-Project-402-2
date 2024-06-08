using SofreDaar.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Client:Base.User
{
    [Required]
    public ClinetSubscription Subscription { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Commnet> Commnets { get; set; }
    public ICollection<Report> Reports { get; set; }
}