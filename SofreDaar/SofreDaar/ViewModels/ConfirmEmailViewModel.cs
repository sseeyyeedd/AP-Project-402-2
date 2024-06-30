using SofreDaar.Infrastructure;
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
                if (!MainVM.LoggedInUser.Activate(Code))
                {
                    //show error
                    return;
                }
                
                MainVM.SetPasswordCommand.Execute(o);
            });
            
        }
        public string? GetEmail()
        {
            if (MainVM.LoggedInUser is null)
            {
                return "";
            }
            return MainVM.LoggedInUser.Email??"";
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); }
        }
        public ICommand ConfirmCommand { get; set; }
    }
}
