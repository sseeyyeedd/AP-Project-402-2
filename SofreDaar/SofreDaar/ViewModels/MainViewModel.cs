using SofreDaar.Infrastructure;
using SofreDaar.Models.Base;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{

    public class MainViewModel : ObservableObject
    {
        private ObservableObject _currentViewModel;
        public ObservableObject CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public User? LoggedInUser { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand ConfirmAccountCommand { get; set; }
        public ICommand SetPasswordCommand { get; set; }

        public MainViewModel(DatabaseContext DbContext)
        {
            LoggedInUser=null;
            LoginCommand = new RelayCommand(o => CurrentViewModel = new LoginViewModel(DbContext, this));
            SignUpCommand=new RelayCommand(o => CurrentViewModel=new SignUpViewModel(DbContext, this));
            ConfirmAccountCommand=new RelayCommand(o=>CurrentViewModel=new ConfirmEmailViewModel(DbContext, this));
            SetPasswordCommand=new RelayCommand(o=>CurrentViewModel=new SetPasswordViewModel(DbContext, this));
            _currentViewModel = new LoginViewModel(DbContext, this);
        }
    }
}
