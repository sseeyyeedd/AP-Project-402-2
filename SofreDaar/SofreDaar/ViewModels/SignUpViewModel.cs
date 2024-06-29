using SofreDaar.Infrastructure;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel(DatabaseContext databaseContext, MainViewModel main) : base(databaseContext, main)
        {
            _name="";
            _lastName="";
            _username="";
            _email="";
            SendCodeCommand =new RelayCommand(o =>
            {
                //Check all Inputs
                //if inputs are correct make a user and put it into loggedinuser
                //send verification code
                MainVM.ConfirmAccountCommand.Execute(o);
            });
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
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
        public ICommand SendCodeCommand { get; set; }
    }
}
