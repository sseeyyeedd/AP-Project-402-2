using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.Models
{
    public class Rating:Base.Entity
    {
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public int Star { get; set; }
    }
}
