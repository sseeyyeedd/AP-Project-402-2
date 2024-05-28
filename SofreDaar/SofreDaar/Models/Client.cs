using SofreDaar.Models.Base;

namespace SofreDaar.Models;

public class Client:Base.User
{
    public ClinetSubscription Subscription { get; set; }
    public ICollection<Order> Orders { get; set; }
}