using SofreDaar.Infrastructure;
using SofreDaar.Models;
using SofreDaar.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public delegate void ChangeCurrentPage();
    public class DashboardViewModel:BaseViewModel
    {
        public DashboardViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            ChangeNavigationCommand=new RelayCommand(o =>
            {
                CurrentPage.Change();
            });
            
            NavigationItems.Clear();
            if (MainVM.LoggedInUser is Client)
            {
                NavigationItems.Add(new NavigationItem("پروفایل", Profile));
            }
            else if (MainVM.LoggedInUser is Restaurant)
            {

            }
            else if (MainVM.LoggedInUser is Admin)
            {
                NavigationItems.Add(new NavigationItem("مدیریت رستوران", RestaurantManagment));
            }
        }
        public ObservableCollection<NavigationItem> NavigationItems { get; set; } = [];
        private ObservableObject _currentViewModel;

        public ObservableObject CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        private NavigationItem _currentPage;

        public NavigationItem CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public ICommand ChangeNavigationCommand { get; set; }
        void Profile()
        {
            CurrentViewModel=new ProfileViewModel(Context, MainVM);
        }
        void RestaurantManagment()
        {
            CurrentViewModel=new RestaurantManagmentViewModel(Context, MainVM);
        }
    }
    public class NavigationItem
    {
        public NavigationItem(string? text, ChangeCurrentPage change)
        {
            Text=text;
            Change=change;
        }
        public string? Text { get; set; }
        public ChangeCurrentPage Change;
    }
}
