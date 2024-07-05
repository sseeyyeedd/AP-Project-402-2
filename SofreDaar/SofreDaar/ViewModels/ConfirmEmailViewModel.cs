using SofreDaar.Helpers;
using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class ConfirmEmailViewModel : BaseViewModel
    {
        public ConfirmEmailViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            _code="";
            ConfirmCommand = new RelayCommand(o =>
            {
                if (!((Client)MainVM.LoggedInUser).Activate(Code))
                {
                    //show error
                    return;
                }

                MainVM.SetPasswordCommand.Execute(o);
            });
            ResendCommand=new RelayCommand(o =>
            {
                ((Client)MainVM.LoggedInUser).VerificationCode = Helpers.Email.GenerateVerificationCode();
                Helpers.Email.SendVerificationEmailAsync(GetEmail(), ((Client)MainVM.LoggedInUser).VerificationCode);
            });
            BackCommand=new RelayCommand(o =>
            {
                MainVM.SignUpCommand.Execute(o);
            });
        }
        public string? GetEmail()
        {
            if (MainVM.LoggedInUser is null)
            {
                return "";
            }
            return ((Client)MainVM.LoggedInUser).Email??"";
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); }
        }
        public ICommand ConfirmCommand { get; set; }
        public ICommand ResendCommand { get; set; }
        public ICommand BackCommand { get; set; }
    }
}
