using SofreDaar.Infrastructure;
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

        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        //public ICommand SettingsCommand { get; set; }
        
        public MainViewModel(DatabaseContext DbContext)
        {
            {
                LoginCommand = new RelayCommand(o => CurrentViewModel = new LoginViewModel(DbContext, this));
                SignUpCommand=new RelayCommand(o => CurrentViewModel=new SignUpViewModel(DbContext, this));
                CurrentViewModel = new LoginViewModel(DbContext, this);
            }

        }
    }
}
