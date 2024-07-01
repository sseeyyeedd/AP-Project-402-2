using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SofreDaar.Models;

namespace SofreDaar.ViewModels
{
    public class SetPasswordViewModel : BaseViewModel
    {
        public SetPasswordViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            _password="";
            _repeatPassword="";
            SetPasswordCommand = new RelayCommand(o =>
            {
                if (Password != RepeatPassword)
                {
                    //show error
                    return;
                }

                MainVM.LoggedInUser.Password = Password;
                DbContext.Clients.Add(MainVM.LoggedInUser as Client);
                DbContext.SaveChanges();
                MainVM.LoginCommand.Execute(o);
            });

        }
        private string _password;
        
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        
        private string _repeatPassword;
        
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { _repeatPassword = value; OnPropertyChanged(); }
        }
        
        public ICommand SetPasswordCommand { get; set; }
    }
}
