using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SofreDaar.Models.Base
{
    public class User : Entity
    {
        [Required,MaxLength(63)]
        public string? Username { get; set; }
        [Required,MaxLength(127)]
        public string? Name { get; set; }
        [MaxLength(63)]
        public string? Password { get; set; }
    }
}
