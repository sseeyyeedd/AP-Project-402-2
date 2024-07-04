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
    public class ReportManagementViewModel : BaseViewModel
    {
        public ReportManagementViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            
        }
        public void UpdateReport(Report report)
        {
            Context.Reports.Update(report);
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

        public ObservableCollection<string> FilterReports { get; set; } = [];
        private Restaurant? _restaurant;

        public Restaurant? Restaurant
        {
            get { return _restaurant; }
            set { _restaurant = value; OnPropertyChanged(); }
        }
        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(); }
        }
        public ICommand SearchCommand { get; set; }
    }
}
