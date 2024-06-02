using SofreDaar.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //public ICommand SettingsCommand { get; set; }

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(o => CurrentViewModel = new LoginViewModel());

            CurrentViewModel = new LoginViewModel();
        }

    }
}
