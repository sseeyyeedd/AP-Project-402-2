using Microsoft.EntityFrameworkCore;
using SofreDaar.Helpers;
using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class CartManagmentViewModel : BaseViewModel
    {
        public CartManagmentViewModel(DatabaseContext DbContext, MainViewModel main, DashboardViewModel dashboard, List<OrderItem> items, Restaurant restaurant) : base(DbContext, main)
        {
            Date=null;
            OrderItems=new ObservableCollection<OrderItem>(items);
            CanReserve=false;
            IsReserve=false;
            RName=restaurant.Name;
            Price=items.Sum(x => x.Food.Price*x.Count);
            var client = MainVM.LoggedInUser as Client;
            if (client.ReservesLeft<1||IsMoreThanAMonthAgo(client.SubscriptionStart))
            {
                client.Subscription=Models.Base.ClinetSubscription.Normal;
                client.ReservesLeft=0;
                Context.Clients.Update(client);
                Context.SaveChanges();
            }
            if (restaurant.ReceptionType!=Models.Base.RestaurantReceptionType.DineIn&&client.Subscription!=Models.Base.ClinetSubscription.Normal)
            {
                CanReserve=true;
            }
            Func<bool, bool> SaveData = (bool isInCash) =>
            {
                if (IsReserve)
                {
                    client.ReservesLeft--;
                    Context.Clients.Update(client);
                }
                var newId = Guid.NewGuid();
                var order = new Order()
                {
                    Id=newId,
                    ClientId=MainVM.LoggedInUser.Id,
                    RestaurantId=restaurant.Id,
                    DateTime=DateTime.Now,
                    IsPaymentInCash=isInCash,
                    ReserveStatus=IsReserve ? Models.Base.ReserveStatus.Reserve : Models.Base.ReserveStatus.NotReserve,
                    ReserveDateTime= IsReserve?((DateTime)Date).AddHours(Hours).AddMinutes(Minutes):DateTime.MinValue,
                    OrderComment="",
                    PaymentValue=Price,
                    CancelDateTime=DateTime.MinValue,
                };
                Context.Orders.Add(order);
                Context.SaveChanges();
                foreach (var item in items)
                {
                    item.Food.Stock-=item.Count;
                    Context.Foods.Update(item.Food);
                    item.OrderId=newId;
                    Context.OrderItems.Add(item);
                }
                Context.SaveChanges();
                return true;
            };
            OnlinePaymentCommand=new RelayCommand(o =>
            {
                if (IsReserve&&Date is null)
                {
                    return;
                }
                SaveData(false);
                Email.SendOrderItemEmail(client.Email, items);
                dashboard.CurrentViewModel=null;
            });
            CashPaymentCommand=new RelayCommand(o =>
            {
                if (IsReserve&&Date is null)
                {
                    return;
                }
                SaveData(true);
                dashboard.CurrentViewModel=null;
            });
           
        }
        bool IsMoreThanAMonthAgo(DateTime dateToCheck)
        {
            DateTime currentDate = DateTime.Now;
            int monthDifference = ((currentDate.Year - dateToCheck.Year) * 12) + currentDate.Month - dateToCheck.Month;
            return monthDifference > 1 || (monthDifference == 1 && currentDate.Day >= dateToCheck.Day);
        }
        public ObservableCollection<OrderItem> OrderItems { get; set; } = [];


        private string _rname;

        public string RName
        {
            get { return _rname; }
            set { _rname = value; OnPropertyChanged(); }
        }

        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }
        private bool _CanReserve;

        public bool CanReserve
        {
            get { return _CanReserve; }
            set { _CanReserve = value; OnPropertyChanged(); }
        }
        private bool _isReserve;

        public bool IsReserve
        {
            get { return _isReserve; }
            set { _isReserve = value; OnPropertyChanged(); }
        }
        private DateTime? _date;

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }
        private int _minutes;

        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; OnPropertyChanged(); }
        }
        private int _hours;

        public int Hours
        {
            get { return _hours; }
            set { _hours = value; OnPropertyChanged(); }
        }


        public ICommand OnlinePaymentCommand { get; set; }
        public ICommand CashPaymentCommand { get; set; }

    }

}
