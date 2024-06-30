using SofreDaar.Infrastructure;
using SofreDaar.Models;
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
            SignUpCommand = new RelayCommand(o =>
            {
                MainVM.SignUpCommand.Execute(this);
            });
			LoginCommand=new RelayCommand(o=>{
				MainVM.LoggedInUser= DbContext.Clients.FirstOrDefault(x=>x.Username==Username&&x.Password==Password);
                MainVM.LoggedInUser??= DbContext.Restaurants.FirstOrDefault(x => x.Username==Username&&x.Password==Password);
                MainVM.LoggedInUser??= DbContext.Admins.FirstOrDefault(x => x.Username==Username&&x.Password==Password);
                
				if (MainVM.LoggedInUser is null)
				{
					//wrong username or password error
				}
				else if(MainVM.LoggedInUser is Client) 
				{
                    //navigate to client dashboard
                }
                else if (MainVM.LoggedInUser is Restaurant)
                {
                    //navigate to restaurant dashboard 
                }
                else if (MainVM.LoggedInUser is Admin)
                {
                    //navigate to admin dashboard 
                }
				MainVM.LoginCommand.Execute(o);
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
        public ICommand SignUpCommand { get; set; }

    }
}
