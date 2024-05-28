using SofreDaar.Models.Base;

namespace SofreDaar.Models;

public class Food:Base.Entity
{
    public FoodType FoodType { get; set; }
}