﻿using SofreDaar.Models.Base;
namespace SofreDaar.Models;

public class Restaurant:Base.Entity
{
    public string Name { get; set; }
    public string City { get; set; }
    public RestaurantReceptionType ReceptionType { get; set; }
    public string Address { get; set; }
    
    
}