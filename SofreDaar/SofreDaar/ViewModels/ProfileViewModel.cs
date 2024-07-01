using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public ProfileViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            
        }
    }
}
