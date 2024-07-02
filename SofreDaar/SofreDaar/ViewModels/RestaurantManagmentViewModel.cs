using Microsoft.EntityFrameworkCore;
using SofreDaar.Helpers;
using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class RestaurantManagmentViewModel:BaseViewModel
    {
        public RestaurantManagmentViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            Restaurants=new ObservableCollection<Restaurant>(DbContext.Restaurants);
            Restaurants??= [];
            AddRestaurantCommand=new RelayCommand(o =>
            {
                if (!Name.IsName())
                {
                    //show error
                    return;
                }
                if (!Username.IsUsername()||DbContext.Clients.Any(x => x.Username==Username)||DbContext.Restaurants.Any(x => x.Username==Username)||DbContext.Admins.Any(x => x.Username==Username))
                {
                    //show error
                    return;
                }
                var restaurant = new Restaurant()
                {
                    Id=Guid.NewGuid(),
                    Name=Name,
                    Username=Username,
                    Password=Password,
                    City=City,
                    Address="",
                    ReceptionType=Models.Base.RestaurantReceptionType.DineIn
                };
                DbContext.Restaurants.Add(restaurant);
                DbContext.SaveChanges();
                Restaurants.Add(restaurant);
                Name="";
                Username="";
                Password="";
                City="";
            });
            
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private string _nameSearch;

        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged(); }
        }
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }
        private string _citySearch;

        public string CitySearch
        {
            get { return _citySearch; }
            set { _citySearch = value; OnPropertyChanged(); }
        }
        private double _rating;

        public double RatingSearch
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(); }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        public void UpdatePassword(Restaurant restaurant)
        {
            Context.Restaurants.Update(restaurant);
            Context.SaveChanges();
        }
        public ICommand AddRestaurantCommand { get; set; }
        public ObservableCollection<Restaurant> Restaurants { get; set; }
    }
}
