using SofreDaar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SofreDaar.Models;
using SofreDaar.Models.Base;
using SofreDaar.Helpers;
using System.Windows.Forms;
using System.Windows;

namespace SofreDaar.ViewModels
{
    public class ReceptionViewModel : BaseViewModel
    {
        public ReceptionViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            CanDisableReserve=(MainVM.LoggedInUser as Restaurant).ReceptionType!=RestaurantReceptionType.DineIn;
            Context.Entry((MainVM.LoggedInUser as Restaurant)).Collection(x=>x.Foods).Load();
            foreach(var food in (MainVM.LoggedInUser as Restaurant).Foods)
            {
                Context.Entry(food).Collection(x=>x.Ratings).Load();
            }
            if ((MainVM.LoggedInUser as Restaurant).Foods.Any(x=>x.Ratings.Count>0))
            {
                CanEnableReserve=(MainVM.LoggedInUser as Restaurant).Foods.Average(x => x.Ratings.Average(y => y.Star))>4.5&&(MainVM.LoggedInUser as Restaurant).ReceptionType==RestaurantReceptionType.DineIn;
            }
            else
            {
                CanEnableReserve=false;
            }
            
            EnableReserveCommand=new RelayCommand(o =>
            {
                var Result = System.Windows.MessageBox.Show("سرویس عادی غیرفعال شود؟","", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result==MessageBoxResult.Yes)
                {
                    (MainVM.LoggedInUser as Restaurant).ReceptionType=RestaurantReceptionType.Delivery;
                }
                else
                {
                    (MainVM.LoggedInUser as Restaurant).ReceptionType=RestaurantReceptionType.DeliveryAndDineIn;
                }
                CanDisableReserve=true;
                CanEnableReserve=false;
                Context.Restaurants.Update((MainVM.LoggedInUser as Restaurant));
                Context.SaveChanges();
                
            });
            DisableReserveCommand=new RelayCommand(o =>
            {

                (MainVM.LoggedInUser as Restaurant).ReceptionType=RestaurantReceptionType.DineIn;
                CanDisableReserve=false;
                CanEnableReserve=(MainVM.LoggedInUser as Restaurant).Foods.Average(x => x.Ratings.Average(y => y.Star))>4.5&&(MainVM.LoggedInUser as Restaurant).ReceptionType==RestaurantReceptionType.DineIn;
                Context.Restaurants.Update((MainVM.LoggedInUser as Restaurant));
                Context.SaveChanges();
            });
        }
        private bool _canEnableReserve;

        public bool CanEnableReserve
        {
            get { return _canEnableReserve; }
            set { _canEnableReserve = value; OnPropertyChanged(); }
        }
        private bool _canDisableReserve;

        public bool CanDisableReserve
        {
            get { return _canDisableReserve; }
            set { _canDisableReserve = value; OnPropertyChanged(); }
        }
        private bool _canEnableDineIn;

        public ICommand EnableReserveCommand { get; set; }
        public ICommand DisableReserveCommand { get; set; }
        

    }
        
}
