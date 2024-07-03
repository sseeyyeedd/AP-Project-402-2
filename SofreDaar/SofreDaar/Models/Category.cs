using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.Models
{
    public class Category:Base.Entity
    {
        public string Title { get; set; }
        public ICollection<Food> Foods { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
