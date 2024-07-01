using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.Models
{
    public class OrderItem:Base.Entity
    {
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
