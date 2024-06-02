using SofreDaar.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {
            Username="";

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


	}
}
