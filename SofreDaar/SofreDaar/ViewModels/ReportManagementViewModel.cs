using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SofreDaar.ViewModels
{
    public class ReportManagementViewModel : BaseViewModel
    {
        public ReportManagementViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            FilterReports.Add("همه");
            FilterReports.Add("بررسی شده");
            FilterReports.Add("درحال بررسی");
            var array=FilterReports.ToArray();
            Filter=array[0];
            Reports=new ObservableCollection<Report>(Context.Reports);
            foreach (var item in Reports)
            {
                item.Restaurant=Context.Restaurants.FirstOrDefault(x => x.Id==item.RestaurantId);
                item.Client=Context.Clients.FirstOrDefault(x => x.Id==item.ClientId);
            }
            Title="";
            NameAndFamily="";
            UserName="";
            RestaurantName="";
            SearchCommand = new RelayCommand(o =>
            {
                Reports=new ObservableCollection<Report>(Context.Reports);

                var array = FilterReports.ToArray();
                foreach (var item in Reports)
                {
                    item.Restaurant=Context.Restaurants.FirstOrDefault(x => x.Id==item.RestaurantId);
                    item.Client=Context.Clients.FirstOrDefault(x => x.Id==item.ClientId);
                }
                if (Filter == array[1])
                {
                    Reports=new ObservableCollection<Report>(
                        Reports.Where(x => x.IsFollowedUp&& string.Join(x.Client.Name, " ", x.Client.SureName).Contains(NameAndFamily)&&x.Client.Username.Contains(UserName)&&x.Restaurant.Name.Contains(RestaurantName)&&x.Title.Contains(Title)));
                }
                else if (Filter == array[2])
                {
                    Reports=new ObservableCollection<Report>(
                       Reports.Where(x => !x.IsFollowedUp&& string.Join(x.Client.Name, " ", x.Client.SureName).Contains(NameAndFamily)&&x.Client.Username.Contains(UserName)&&x.Restaurant.Name.Contains(RestaurantName)&&x.Title.Contains(Title)));
                }
                else
                {
                    Reports=new ObservableCollection<Report>(
                      Reports.Where(x => string.Join(x.Client.Name," ",x.Client.SureName).Contains(NameAndFamily)&&x.Client.Username.Contains(UserName)&&x.Restaurant.Name.Contains(RestaurantName)&&x.Title.Contains(Title)));
                }

            });
        }
        private ObservableCollection<Report> _reports;

        public ObservableCollection<Report> Reports
        {
            get { return _reports; }
            set { _reports = value;OnPropertyChanged(); }
        }

        public void UpdateReport(Report report)
        {
            Context.Reports.Update(report);
            Context.SaveChanges();
        }
        
        private string _nameAndFamily;
        public string NameAndFamily
        {
            get { return _nameAndFamily; }
            set
            {
                _nameAndFamily = value;
                OnPropertyChanged();
            }
        }
        private string _restaurantName;
        public string RestaurantName
        {
            get { return _restaurantName; }
            set
            {
                _restaurantName = value;
                OnPropertyChanged();
            }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> FilterReports { get; set; } = [];
        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(); }
        }
        public ICommand SearchCommand { get; set; }
    }
}
