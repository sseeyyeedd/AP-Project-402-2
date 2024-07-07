using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.Models
{
    public class Report:Base.Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        public Restaurant Restaurant { get; set; }
        [Required]
        public Guid RestaurantId { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsFollowedUp { get; set; }
        public string Answer {  get; set; }
    }
}
