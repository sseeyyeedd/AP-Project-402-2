using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SofreDaar.Models;
using SofreDaar.Models.Base;

namespace SofreDaar.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public ProfileViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            _name = MainVM.LoggedInUser.Name;
            _surname = MainVM.LoggedInUser.SureName;
            _phoneNumber = MainVM.LoggedInUser.PhoneNumber;
            _username = MainVM.LoggedInUser.Username;
            _email = MainVM.LoggedInUser.Email;
            _address = "";
            UpgradeToGoldCommand = new RelayCommand(o => ((Client)(MainVM.LoggedInUser)).Subscription = ClinetSubscription.Gold);
            UpgradeToSilverCommand = new RelayCommand(o => ((Client)(MainVM.LoggedInUser)).Subscription = ClinetSubscription.Silver);
            UpgradeToBronzeCommand = new RelayCommand(o => ((Client)(MainVM.LoggedInUser)).Subscription = ClinetSubscription.Bronze);
            SaveCommand = new RelayCommand(o =>
            {
                MainVM.LoggedInUser.PhoneNumber = PhoneNumber;
                MainVM.LoggedInUser.Username = Username;
                MainVM.LoggedInUser.Email = Email;
                if (MainVM.LoggedInUser is Client)
                {
                    ((Client)MainVM.LoggedInUser).Subscription = ((Client)MainVM.LoggedInUser).Subscription;
                }
                DbContext.SaveChanges();
            });
            


        }
        private string _name;
        
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        
        private string _surname;
        
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(); }
        }
        
        private string _phoneNumber;
        
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }
        
        private string _username;
        
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        
        private string _email;
        
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        
        public string _address;
        
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }
        
        public ICommand UpgradeToGoldCommand { get; set; }
        public ICommand UpgradeToSilverCommand { get; set; }
        public ICommand UpgradeToBronzeCommand { get; set; }
        public ICommand SaveCommand { get; set; }


    }
        
}
