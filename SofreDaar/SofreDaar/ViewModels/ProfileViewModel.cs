using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SofreDaar.Models;
using SofreDaar.Models.Base;
using SofreDaar.Helpers;

namespace SofreDaar.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public ProfileViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            _name = ((Client)MainVM.LoggedInUser).Name;
            _surname = ((Client)MainVM.LoggedInUser).SureName;
            _phoneNumber = ((Client)MainVM.LoggedInUser).PhoneNumber;
            _username = MainVM.LoggedInUser.Username;
            _email = ((Client)MainVM.LoggedInUser).Email;
            _address = ((Client)MainVM.LoggedInUser).PostalAddress??"";
            var g= ((Client)MainVM.LoggedInUser).Gender;
            if (g==Models.Base.Gender.Male)
            {
                _gender="آقا";
            }
            else if (g==Models.Base.Gender.Female)
            {
                _gender="خانم";
            }
            else
            {
                _gender="ثبت نشده";
            }
            UpgradeToGoldCommand = new RelayCommand( o => { ((Client)(MainVM.LoggedInUser)).Subscription = ClinetSubscription.Gold;
                ((Client)(MainVM.LoggedInUser)).ReservesLeft=15;
                ((Client)(MainVM.LoggedInUser)).SubscriptionStart=DateTime.Now;
                 Helpers.Email.SendGoldSubscriptionPaymentEmailAsync(((Client)(MainVM.LoggedInUser)).Email);
                DbContext.SaveChanges();

            });
            UpgradeToSilverCommand = new RelayCommand( o => {((Client)(MainVM.LoggedInUser)).Subscription = ClinetSubscription.Silver;
            ((Client)(MainVM.LoggedInUser)).ReservesLeft=5;
            ((Client)(MainVM.LoggedInUser)).SubscriptionStart=DateTime.Now;
                 Helpers.Email.SendSilverSubscriptionPaymentEmailAsync(((Client)(MainVM.LoggedInUser)).Email);
                DbContext.SaveChanges();

            });
            UpgradeToBronzeCommand = new RelayCommand( o => {((Client)(MainVM.LoggedInUser)).Subscription = ClinetSubscription.Bronze;
                ((Client)(MainVM.LoggedInUser)).ReservesLeft=2;
                ((Client)(MainVM.LoggedInUser)).SubscriptionStart=DateTime.Now;
                 Helpers.Email.SendBrnzeSubscriptionPaymentEmailAsync(((Client)(MainVM.LoggedInUser)).Email);
                DbContext.SaveChanges();

            });
            SaveCommand = new RelayCommand(o =>
            {
                if (!Validation.IsEmail(Email))
                {
                    return;
                }
                ((Client)MainVM.LoggedInUser).Email = Email;
                ((Client)MainVM.LoggedInUser).PostalAddress = Address;
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
        public string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
        }
        public ICommand UpgradeToGoldCommand { get; set; }
        public ICommand UpgradeToSilverCommand { get; set; }
        public ICommand UpgradeToBronzeCommand { get; set; }
        public ICommand SaveCommand { get; set; }


    }
        
}
