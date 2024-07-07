using SofreDaar.Helpers;
using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class OrdersManagementViewModel : BaseViewModel
    {
        public OrdersManagementViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            _fromDateTime=null;
            _toDateTime=null;
            _foodNameSearch="";
            _usernameSearch="";
            _phoneNumberSearch="";
            _fromPrice=0;
            _toPrice=int.MaxValue;
            var orders = Context.Orders.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id);
            if (orders is null)
            {
                Orders= [];
            }
            else
            {
                Orders=new ObservableCollection<Order>(orders);
            }
            CalculateStats();
            FilterCommand=new RelayCommand(o =>
            {
               var  orders1 = Context.Orders.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id).ToList();
                foreach (var item in orders1)
                {
                    Context.Entry(item).Collection(x => x.OrderItems).Load();
                    item.Client=Context.Clients.First(x => x.Id==item.ClientId);
                    foreach (var item1 in item.OrderItems)
                    {
                        item1.Food=Context.Foods.FirstOrDefault(x => x.Id==item1.FoodId);
                    }
                }
                
                Orders=new ObservableCollection<Order>(orders1.Where(x => x.Client.PhoneNumber.Contains(PhoneNumberSearch)&&x.Client.Username.Contains(UsernameSearch)&&x.OrderItems.Any(y => y.Food is not null? y.Food.Name.Contains(FoodNameSearch) : true) &&x.PaymentValue>FromPrice&&x.PaymentValue<ToPrice&&x.DateTime<ToDateTime&&x.DateTime>FromDateTime));
                CalculateStats();
            });
            ExportCommand=new RelayCommand(o =>
            {
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.Filter = "CSV File (*.csv)|*.csv";
                if (dialog.ShowDialog()??false)
                {
                    Export.ExportToCsv(Orders, dialog.FileName);
                }
            });

        }
        void CalculateStats()
        {
            SumPrice=Orders.Sum(x => x.PaymentValue);
            if (Orders.Count!=0)
            {
                OnlinePercentage=(Orders.Where(x => !x.IsPaymentInCash).Count()/Orders.Count)*100;
            }
            else
            {
                OnlinePercentage=0;
            }
            
            ReserveCount=Orders.Where(x => x.ReserveStatus!=Models.Base.ReserveStatus.NotReserve).Count();
            NormalCount=Orders.Where(x => x.ReserveStatus==Models.Base.ReserveStatus.NotReserve).Count();
            CancelCount=Orders.Where(x => x.ReserveStatus==Models.Base.ReserveStatus.Canceled).Count();
            CancelForfeit=Orders.Where(x => x.ReserveStatus==Models.Base.ReserveStatus.Canceled).Sum(y => CalculateForfeit(y));
        }
        int CalculateForfeit(Order order)
        {
            order.Client=Context.Clients.FirstOrDefault(x => x.Id==order.ClientId);
            switch (order.Client.Subscription)
            {
                case Models.Base.ClinetSubscription.Bronze:
                    if (order.CancelDateTime>((DateTime)order.ReserveDateTime).AddMinutes(-30))
                    {
                        return order.PaymentValue;
                    }
                    else
                    {
                        return (int)Math.Floor(order.PaymentValue*0.3);
                    }

                case Models.Base.ClinetSubscription.Silver:
                    if (order.CancelDateTime>((DateTime)order.ReserveDateTime).AddMinutes(-30))
                    {
                        return order.PaymentValue;
                    }
                    else
                    {
                        return (int)Math.Floor(order.PaymentValue*0.3);
                    }
                case Models.Base.ClinetSubscription.Gold:
                    if (order.CancelDateTime>((DateTime)order.ReserveDateTime).AddMinutes(-15))
                    {
                        return order.PaymentValue;
                    }
                    else
                    {
                        return (int)Math.Floor(order.PaymentValue*0.3);
                    }
                default:
                    return 0;
            }
        }
        private string _phoneNumberSearch;

        public string PhoneNumberSearch
        {
            get { return _phoneNumberSearch; }
            set { _phoneNumberSearch = value; OnPropertyChanged(); }
        }
        private string _usernameSearch;

        public string UsernameSearch
        {
            get { return _usernameSearch; }
            set { _usernameSearch = value; OnPropertyChanged(); }
        }
        private string _foodNameSearch;

        public string FoodNameSearch
        {
            get { return _foodNameSearch; }
            set { _foodNameSearch = value; OnPropertyChanged(); }
        }
        private DateTime? _fromDateTime;

        public DateTime? FromDateTime
        {
            get { return _fromDateTime; }
            set { _fromDateTime = value; OnPropertyChanged(); }
        }
        private DateTime? _toDateTime;

        public DateTime? ToDateTime
        {
            get { return _toDateTime; }
            set { _toDateTime = value; OnPropertyChanged(); }
        }
        private int _fromPrice;

        public int FromPrice
        {
            get { return _fromPrice; }
            set { _fromPrice = value; OnPropertyChanged(); }
        }
        private int _toPrice;

        public int ToPrice
        {
            get { return _toPrice; }
            set { _toPrice = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(); }
        }
        private int _sumPrice;

        public int SumPrice
        {
            get { return _sumPrice; }
            set { _sumPrice = value; OnPropertyChanged(); }
        }
        private int _onlinePercentage;

        public int OnlinePercentage
        {
            get { return _onlinePercentage; }
            set { _onlinePercentage = value; OnPropertyChanged(); }
        }
        private int _reserveCount;

        public int ReserveCount
        {
            get { return _reserveCount; }
            set { _reserveCount = value; OnPropertyChanged(); }
        }
        private int _normalCount;

        public int NormalCount
        {
            get { return _normalCount; }
            set { _normalCount = value; OnPropertyChanged(); }
        }
        private int _cancelCount;

        public int CancelCount
        {
            get { return _cancelCount; }
            set { _cancelCount = value; OnPropertyChanged(); }
        }
        private int _cancelForfeit;

        public int CancelForfeit
        {
            get { return _cancelForfeit; }
            set { _cancelForfeit = value; OnPropertyChanged(); }
        }
        public ICommand FilterCommand { get; set; }
        public ICommand ExportCommand { get; set; }
    }
}
