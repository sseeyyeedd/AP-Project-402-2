using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models.Base
{
    public class Entity
    {
        [Key,Required]
        public Guid Id { get; set; }
    }
}
