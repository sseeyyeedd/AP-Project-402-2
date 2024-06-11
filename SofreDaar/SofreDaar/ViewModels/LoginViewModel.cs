using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
        public LoginViewModel(DatabaseContext DbContext,MainViewModel main):base(DbContext,main)
        {
            _username="";
			_password="";
			LoginCommand=new RelayCommand(o=>{
				
            });
			
        }
		private string _username;

		public string Username
		{
			get { return _username; }
			set { _username = value; OnPropertyChanged(); }
		}
		private string _password;

		public string Password
		{
			get { return _password; }
			set { _password = value; OnPropertyChanged(); }
		}
        public ICommand LoginCommand { get; set; }

    }
}
