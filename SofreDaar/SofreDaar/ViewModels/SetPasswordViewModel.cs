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
            _confirmPassword="";
            SetPasswordCommand = new RelayCommand(o =>
            {
                if (Password != ConfirmPassword)
                {
                    //show error
                    return;
                }

                MainVM.LoggedInUser.Password = Password;
                DbContext.Clients.Add(MainVM.LoggedInUser as Client);
                MainVM.LoginCommand.Execute(o);
            });

        }
        private string _password;
        
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        
        private string _confirmPassword;
        
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
        }
        
        public ICommand SetPasswordCommand { get; set; }
    }
}
