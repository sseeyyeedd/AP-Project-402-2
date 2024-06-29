using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.ViewModels
{
    public class ConfirmEmailViewModel : BaseViewModel
    {
        public ConfirmEmailViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            
        }
        public string? GetEmail()
        {
            if (MainVM.LoggedInUser is null)
            {
                return "";
            }
            return MainVM.LoggedInUser.Email??"";
        }
    }
}
