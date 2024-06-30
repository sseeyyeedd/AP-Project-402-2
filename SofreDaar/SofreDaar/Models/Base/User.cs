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
        [MaxLength(127)]
        public string? SureName { get; set; }
        [MaxLength(63)]
        public string? Password { get; set; }
        public string? Email { get; set; }
        [MaxLength(6)]
        public string? VerificationCode { get; set; }
        [MaxLength(11)]
        public string? PhoneNumber { get; set; }

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
}
