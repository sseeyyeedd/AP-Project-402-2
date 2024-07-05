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
    public class MenuViewModel : BaseViewModel
    {
        public DashboardViewModel Dashboard { get; }
        public MenuViewModel(DatabaseContext DbContext, MainViewModel main, Restaurant restaurant, DashboardViewModel dashboard) : base(DbContext, main)
        {
            Dashboard = dashboard;
            client=MainVM.LoggedInUser as Client;
            NewRating=0;
            FoodCount=1;
            Cart= [];
            var categories = Context.Categorys.Where(x => x.RestaurantId==restaurant.Id);
            var foods = Context.Foods.Where(x => x.RestaurantId==restaurant.Id&&x.Stock>0);
            if (categories is null)
            {
                Categories= [];
            }
            else
            {
                Categories=new ObservableCollection<Category>(categories);
            }
            if (foods is null)
            {
                MenuFoods= [];
            }
            else
            {
                MenuFoods=new ObservableCollection<Food>(foods);
            }
            AddStarCommand=new RelayCommand(o =>
            {
                if (NewRating<5)
                {
                    NewRating++;
                    UpdateRating();
                }
            });
            RemoveStarCommand=new RelayCommand(o =>
            {
                if (NewRating>0)
                {
                    NewRating--;
                    UpdateRating();
                }
            });
            AddFoodCommand=new RelayCommand(o =>
            {
                if (FoodCount<CurrentFood.Stock)
                {
                    FoodCount++;
                    
                }
            });
            RemoveFoodCommand=new RelayCommand(o =>
            {
                if (FoodCount>0)
                {
                    FoodCount--;
                }
            });
            AddToCartCommand=new RelayCommand(o =>
            {
                if (Cart.Any(x => x.FoodId==CurrentFood.Id))
                {
                    Cart.First(x => x.FoodId==CurrentFood.Id).Count=FoodCount;
                }
                else
                {
                    var item = new OrderItem()
                    {
                        Id=Guid.NewGuid(),
                        FoodId=CurrentFood.Id,
                        Food=CurrentFood,
                        Count=FoodCount,
                    };
                    Cart.Add(item);
                }
                SetNull();
            });
            CartCommand=new RelayCommand(o =>
            {
                if (Cart.Count==0)
                {
                    return;
                }
                dashboard.CurrentViewModel=new CartManagmentViewModel(Context, MainVM, Dashboard, Cart,restaurant);
            });
            ShowCommentsCommand=new RelayCommand(o => {
                if (CurrentFood is not null)
                {
                    dashboard.CurrentViewModel=new CommentsViewModel(DbContext, MainVM, CurrentFood, this, dashboard);
                }
            });
        }
        void SetNull()
        {
            CurrentFood=null;
            IsSelected=false;
            CanRate=false;
        }
        void UpdateRating()
        {
            Context.Entry(CurrentFood).Collection(x => x.Ratings).Load();
            var rate = CurrentFood.Ratings.FirstOrDefault(x => x.ClientId==MainVM.LoggedInUser.Id);
            if (rate!=null)
            {
                rate.Star=NewRating;
                Context.Ratings.Update(rate);
            }
            else
            {
                Models.Rating rating = new Models.Rating()
                {
                    Id=Guid.NewGuid(),
                    ClientId=MainVM.LoggedInUser.Id,
                    FoodId=CurrentFood.Id,
                    Star=NewRating
                };
                Context.Ratings.Add(rating);
            }
            Context.SaveChanges();
        }
        private Client client;
        public List<OrderItem> Cart { get; }
        private Food _currentFood;

        public Food CurrentFood
        {
            get { return _currentFood; }
            set
            {
                _currentFood = value; OnPropertyChanged();
                if (value is not null)
                {
                    if (Cart.Any(x => x.FoodId==CurrentFood.Id))
                    {
                        FoodCount=Cart.First(x => x.FoodId==CurrentFood.Id).Count;
                    }
                    else
                    {
                        FoodCount=1;
                    }
                    Context.Entry(value).Collection(x => x.Ratings).Load();
                    Rating=value.Ratings.Count>0 ? value.Ratings.Average(x => x.Star) : 0;
                    var pRating = value.Ratings.FirstOrDefault(x => x.ClientId==MainVM.LoggedInUser.Id);
                    NewRating=pRating is null ? 0 : pRating.Star;
                    Context.Entry(client).Collection(x => x.Orders).Load();
                    foreach (var item in client.Orders)
                    {
                        Context.Entry(item).Collection(x => x.OrderItems).Load();
                    }
                    CanRate=client.Orders.Any(x => x.OrderItems.Any(x => x.FoodId==CurrentFood.Id));
                    IsSelected=true;
                }
            }
        }
        private ObservableCollection<Food> _menuFoods;

        public ObservableCollection<Food> MenuFoods
        {
            get { return _menuFoods; }
            set { _menuFoods = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Category> Categories { get; set; }
        private Category _category;

        public Category Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged();
            MenuFoods.Clear();
            MenuFoods = new ObservableCollection<Food>(Context.Foods.Where(x=>x.CategoryId==_category.Id));
            }
        }
        private double _rating;

        public double Rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(); }
        }
        private int _newRating;

        public int NewRating
        {
            get { return _newRating; }
            set { _newRating = value; OnPropertyChanged(); }
        }
        private int _foodCount;

        public int FoodCount
        {
            get { return _foodCount; }
            set { _foodCount = value; OnPropertyChanged(); }
        }
        private bool _canRate;

        public bool CanRate
        {
            get { return _canRate; }
            set { _canRate = value; OnPropertyChanged(); }
        }
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }
        public ICommand AddStarCommand { get; set; }
        public ICommand RemoveStarCommand { get; set; }
        public ICommand AddFoodCommand { get; set; }
        public ICommand RemoveFoodCommand { get; set; }
        public ICommand AddToCartCommand { get; set; }
        public ICommand CartCommand { get; set; }
        public ICommand ShowCommentsCommand { get; set; }
    }
}
