using SofreDaar.Infrastructure;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SofreDaar.Helpers;
using SofreDaar.Models;


namespace SofreDaar.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            _name="";
            _sureName="";
            _username="";
            _email="";
            _phoneNumber="";
            _verificationCode="";
            SendCodeCommand =new RelayCommand(o =>
            {
                //Check all Inputs
                
                //name is not valid
                if (!Name.IsName())
                {
                    //show error
                    return;
                }
                //last name is not valid
                if (!SureName.IsName())
                {
                    //show error
                    return;
                }
                //username is not valid and is not unique
                if (!Username.IsUsername()||DbContext.Clients.Any(x=>x.Username==Username)||DbContext.Restaurants.Any(x=>x.Username==Username)||DbContext.Admins.Any(x=>x.Username==Username))
                {
                    //show error
                    return;
                }
                //email is not valid and is not unique
                if (!Email.IsEmail()||DbContext.Clients.Any(x=>x.Email==Email)||DbContext.Restaurants.Any(x=>x.Email==Email)||DbContext.Admins.Any(x=>x.Email==Email))
                {
                    //show error
                    return;
                }
                //phone number is not valid and is not unique
                if (!PhoneNumber.IsPhoneNumber()||DbContext.Clients.Any(x=>x.PhoneNumber==PhoneNumber)||DbContext.Restaurants.Any(x=>x.PhoneNumber==PhoneNumber))
                {
                    //show error
                    return;
                }
                //if inputs are correct make a user and put it into loggedinuser
                MainVM.LoggedInUser=new Client{Name=Name,SureName= SureName,Username=Username,PhoneNumber = PhoneNumber,Email=Email};
                //send verification code
                VerificationCode = Helpers.Email.GenerateVerificationCode();
                Helpers.Email.SendVerificationEmail(Email, VerificationCode);
                
                MainVM.ConfirmAccountCommand.Execute(o);
            });
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private string _sureName;

        public string SureName
        {
            get { return _sureName; }
            set
            {
                _sureName = value;
                OnPropertyChanged();
            }
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
            set
            {
                _email = value;
                OnPropertyChanged(); 
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        private string _verificationCode;

        public string VerificationCode
        {
            get { return _verificationCode; }
            set { _verificationCode = value; OnPropertyChanged(); }
        }
        public ICommand SendCodeCommand { get; set; }
    }
}
