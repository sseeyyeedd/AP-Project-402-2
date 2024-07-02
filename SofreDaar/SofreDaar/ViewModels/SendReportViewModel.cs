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
    public class SendReportViewModel : BaseViewModel
    {
        public SendReportViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            Restaurants =new ObservableCollection<Restaurant>(DbContext.Restaurants);
            var reports = Context.Reports.Where(x => x.Client==MainVM.LoggedInUser as Client);
            Title="";
            Description="";
            Restaurant=null;

            if (reports is null)
            {
                Reports= [];
            }
            else
            {
                Reports=new ObservableCollection<Report>(reports);
            }

            FilterReports.Add("همه");
            FilterReports.Add("بررسی شده");
            FilterReports.Add("درحال بررسی");
            AddReportCommand=new RelayCommand(o =>
            {
                if (Restaurant==null)
                {
                    return;
                }
                var newReport=new Report()
                {
                    Id=Guid.NewGuid(),
                    Title=Title,
                    Description=Description,
                    ClientId=MainVM.LoggedInUser.Id,
                    RestaurantId=Restaurant.Id,
                    Answer="",
                    IsFollowedUp=false,
                    DateTime=DateTime.Now,
                };
                Reports.Add(newReport);
                Context.Reports.Add(newReport);
                Context.SaveChanges();
                Title="";
                Description="";
                Restaurant=null;
            });
            FilterCommand = new RelayCommand(o =>
            {
                var reports = Context.Reports.Where(x => x.Client==MainVM.LoggedInUser as Client);
                if (reports is null)
                {
                    Reports= [];
                }
                else
                {
                    Reports=new ObservableCollection<Report>(reports);
                }

                var array = FilterReports.ToArray();
                if (Filter == array[1])
                {
                    Reports=new ObservableCollection<Report>(
                        Reports.Where(x => x.IsFollowedUp));
                }
                else if (Filter == array[2])
                {
                    Reports=new ObservableCollection<Report>(
                       Reports.Where(x => !x.IsFollowedUp));
                }
                
            });
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Restaurant> Restaurants { get; set; } = [];
        private Restaurant? _restaurant;

        public Restaurant? Restaurant
        {
            get { return _restaurant; }
            set { _restaurant = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> FilterReports { get; set; } = [];
        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Report> _reports;

        public ObservableCollection<Report> Reports
        {
            get { return _reports; }
            set { _reports = value; OnPropertyChanged(); }
        }
        public ICommand AddReportCommand { get; set; }
        public ICommand FilterCommand { get; set; }
    }
}
