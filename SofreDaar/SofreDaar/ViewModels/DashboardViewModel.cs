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
                NavigationItems.Add(new NavigationItem("ارسال گزارش", SendReport));
                NavigationItems.Add(new NavigationItem("جستجوی رستوران", RestaurantSearch));
                NavigationItems.Add(new NavigationItem("سفارشات", Orders));
            }
            else if (MainVM.LoggedInUser is Restaurant)
            {
                NavigationItems.Add(new NavigationItem("مدیریت دسته ها", CategoryManagment));
                NavigationItems.Add(new NavigationItem("مدیریت منو", MenuManagment));
                NavigationItems.Add(new NavigationItem("مدیریت سرویس رزرو", Reception));
                NavigationItems.Add(new NavigationItem("سفارشات", OrdersManagment));
            }
            else if (MainVM.LoggedInUser is Admin)
            {
                NavigationItems.Add(new NavigationItem("مدیریت رستوران", RestaurantManagment));
                NavigationItems.Add(new NavigationItem("مدیریت گزارشات", ReportManagement));
            }
            NavigationItems.Add(new NavigationItem("خروج", Logout));
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
        void ReportManagement()
        {
            CurrentViewModel=new ReportManagementViewModel(Context, MainVM);
        }
        void RestaurantSearch()
        {
            CurrentViewModel=new RestaurantSearchViewModel(Context, MainVM,this);
        }
        void CategoryManagment()
        {
            CurrentViewModel=new CategoryManagementViewModel(Context, MainVM);
        }
        void MenuManagment()
        {
            CurrentViewModel=new MenuManagementViewModel(Context, MainVM,this);
        }
        void SendReport()
        {
            CurrentViewModel=new SendReportViewModel(Context, MainVM);
        }
        void Orders()
        {
            CurrentViewModel=new OrdersViewModel(Context, MainVM);
        }
        void Reception()
        {
            CurrentViewModel=new ReceptionViewModel(Context, MainVM);
        }
        void OrdersManagment()
        {
            CurrentViewModel=new OrdersManagementViewModel(Context, MainVM);
        }
        void Logout()
        {
            MainVM.LoggedInUser=null;
            MainVM.LoginCommand.Execute(this);
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
