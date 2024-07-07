using SofreDaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.Infrastructure
{
    public class BaseViewModel:ObservableObject
    {
        protected readonly MainViewModel MainVM;
        protected readonly DatabaseContext Context;
        public BaseViewModel(DatabaseContext databaseContext,MainViewModel main)
        {
            Context = databaseContext;
            MainVM = main;
        }
    }
}
