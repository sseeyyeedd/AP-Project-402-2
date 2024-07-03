using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class CategoryManagementViewModel:BaseViewModel
    {
        public CategoryManagementViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            
        }
        public ICommand SaveDataCommand { get; set; }
    }
}
