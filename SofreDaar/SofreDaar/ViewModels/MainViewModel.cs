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
        public MainViewModel()
        {
            MyProperty="ajsjosjodsd";
        }
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
        private string _myProperty;

        public string MyProperty
        {
            get { return _myProperty; }
            set
            {
                _myProperty = value;
                OnPropertyChanged();
            }
        }
        //public ICommand HomeCommand { get; set; }
        //public ICommand SettingsCommand { get; set; }

        //public MainViewModel()
        //{
        //    HomeCommand = new RelayCommand(o => CurrentViewModel = new HomeViewModel());
        //    SettingsCommand = new RelayCommand(o => CurrentViewModel = new SettingsViewModel());

        //    // Set default ViewModel
        //    CurrentViewModel = new HomeViewModel();
        //}

    }
}
