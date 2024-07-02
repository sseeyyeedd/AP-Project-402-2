using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models;

public class Commnet : Base.Entity
{
    public string Text { get; set; }
    public Food Food { get; set; }
    public Guid FoodId { get; set; }
    public Client Client { get; set; }
    [Required]
    public Guid ClientId { get; set; }
    public Commnet ReplayTo { get; set; }
    public Guid ReplayToId { get; set; }
    public DateTime DateTime { get; set; }
    public ICollection<Commnet> Replays { get; set; }
    public bool isEdited { get; set; }

}