using Microsoft.EntityFrameworkCore;
using SofreDaar.Helpers;
using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class RestaurantSearchViewModel : BaseViewModel
    {
        public DashboardViewModel Dashboard { get;  }
        public RestaurantSearchViewModel(DatabaseContext DbContext, MainViewModel main,DashboardViewModel dashboard) : base(DbContext, main)
        {
            Dashboard = dashboard;
            Filters.Add("همه");
            Filters.Add("هردو");
            Filters.Add("معمولی");
            Filters.Add("رزرو");
            RatingSearch=0;
            Name="";
            NameSearch="";
            Username="";
            City="";
            CitySearch="";
            Filter=Filters[0];
            Restaurants =new ObservableCollection<Restaurant>(DbContext.Restaurants);
            Restaurants??= [];
            SearchCommand = new RelayCommand(o =>
{
    Restaurants =new ObservableCollection<Restaurant>(DbContext.Restaurants);
    foreach (var item in Restaurants)
    {
        Context.Entry(item).Collection(x => x.Foods).Load();
        foreach (var item1 in item.Foods)
        {
            Context.Entry(item1).Collection(x => x.Ratings).Load();
        }
    }
    var array = Filters.ToArray();
    if (Filter == array[1])
    {
        Restaurants = new ObservableCollection<Restaurant>(
            Restaurants.Where(x =>
                 x.ReceptionType==Models.Base.RestaurantReceptionType.DeliveryAndDineIn&&
                x.Name.Contains(NameSearch) &&
                x.City.Contains(CitySearch) &&
                ((x.Foods.Count>0 ? x.Foods.Average(y => y.Ratings.Count>0 ? y.Ratings.Average(z => z.Star) : 0) : 0) >= RatingSearch)
            )
        );
    }
    else if (Filter == array[2])
    {
        Restaurants = new ObservableCollection<Restaurant>(
            Restaurants.Where(x =>
                x.ReceptionType!=Models.Base.RestaurantReceptionType.Delivery&&
                x.Name.Contains(NameSearch) &&
                x.City.Contains(CitySearch) &&
                ((x.Foods.Count>0 ? x.Foods.Average(y => y.Ratings.Count>0 ? y.Ratings.Average(z => z.Star) : 0) : 0) >= RatingSearch)
            )
        );
    }
    else if (Filter == array[3])
    {
        Restaurants = new ObservableCollection<Restaurant>(
            Restaurants.Where(x =>
                x.ReceptionType!=Models.Base.RestaurantReceptionType.DineIn&&
                x.Name.Contains(NameSearch) &&
                x.City.Contains(CitySearch) &&
                ((x.Foods.Count>0 ? x.Foods.Average(y => y.Ratings.Count>0 ? y.Ratings.Average(z => z.Star) : 0) : 0) >= RatingSearch)
            )
        );
    }
    else
    {
        Restaurants = new ObservableCollection<Restaurant>(
            Restaurants.Where(x =>
                x.Name.Contains(NameSearch) &&
                x.City.Contains(CitySearch) &&
                ((x.Foods.Count>0 ? x.Foods.Average(y => y.Ratings.Count>0 ? y.Ratings.Average(z => z.Star) : 0) : 0) >= RatingSearch)
            )
        );
    }
});

        }
        public ObservableCollection<string> Filters { get; set; } = [];
        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(); }
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
        public void UpdatePassword(Restaurant restaurant)
        {
            Context.Restaurants.Update(restaurant);
            Context.SaveChanges();
        }
        public ICommand SearchCommand { get; set; }
        private ObservableCollection<Restaurant> _restaurants;

        public ObservableCollection<Restaurant> Restaurants
        {
            get { return _restaurants; }
            set { _restaurants = value; OnPropertyChanged(); }
        }
        private Restaurant _currentRestaurant;

        public Restaurant CurrentRestaurant
        {
            get { return _currentRestaurant; }
            set { _currentRestaurant = value;
                if (_currentRestaurant is not null)
                {
                    Dashboard.CurrentViewModel=new MenuViewModel(Context,MainVM, _currentRestaurant,Dashboard);
                }
            }
        }


    }

}
