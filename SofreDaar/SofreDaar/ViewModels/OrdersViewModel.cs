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
    public class OrdersViewModel : BaseViewModel
    {
        public OrdersViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            var orders = Context.Orders.Where(x => x.ClientId==MainVM.LoggedInUser.Id);
            if (orders is null)
            {
                Orders= [];
            }
            else
            {
                Orders=new ObservableCollection<Order>(orders);
            }

            CancelReserve=new RelayCommand(o =>
            {
               IsCanceled = true;
                CanCancel=false;
                CurrentOrder.CancelDateTime = DateTime.Now;
                CurrentOrder.ReserveStatus=Models.Base.ReserveStatus.Canceled;
                Forfeit=CalculateForfeit();
                Context.Entry(CurrentOrder).Collection(x => x.OrderItems).Load();
                foreach (var item in CurrentOrder.OrderItems)
                {
                    var food=Context.Foods.FirstOrDefault(x=>x.Id==item.FoodId);
                    if (food==null)
                    {
                        continue;
                    }
                    food.Stock+=item.Count;
                    Context.Foods.Update(food);
                }
                Context.Orders.Update(CurrentOrder);
                Context.SaveChanges();
            });
           
        }
        private int _forfeit;

        public int Forfeit
        {
            get { return _forfeit; }
            set { _forfeit = value; OnPropertyChanged(); }
        }
        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set { _comment = value;OnPropertyChanged(); SaveComment(); }
        }
        private bool _canCancel;

        public bool CanCancel
        {
            get { return _canCancel; }
            set { _canCancel = value; OnPropertyChanged(); }
        }
        private bool _isCanceled;

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set { _isCanceled = value; OnPropertyChanged(); }
        }
        private Order _currentOrder;

        public Order CurrentOrder
        {
            get { return _currentOrder; }
            set { _currentOrder = value; OnPropertyChanged();
                if (_currentOrder is not null)
                {
                    Comment=value.OrderComment;
                    CanCancel=value.ReserveStatus==Models.Base.ReserveStatus.Reserve;
                    IsCanceled=value.ReserveStatus==Models.Base.ReserveStatus.Canceled;
                    if (IsCanceled)
                    {
                        Forfeit=CalculateForfeit();
                    }
                }
                else
                {
                    CanCancel=false;
                    IsCanceled=false;
                }
                
            }
        }
        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(); }
        }
        int CalculateForfeit()
        {
            switch ((MainVM.LoggedInUser as Client).Subscription)
            {
                case Models.Base.ClinetSubscription.Bronze:
                    if (CurrentOrder.CancelDateTime>((DateTime)CurrentOrder.ReserveDateTime).AddMinutes(-30))
                    {
                        return CurrentOrder.PaymentValue;
                    }
                    else
                    {
                        return (int)Math.Floor(CurrentOrder.PaymentValue*0.3);
                    }
                    
                case Models.Base.ClinetSubscription.Silver:
                    if (CurrentOrder.CancelDateTime>((DateTime)CurrentOrder.ReserveDateTime).AddMinutes(-30))
                    {
                        return CurrentOrder.PaymentValue;
                    }
                    else
                    {
                        return (int)Math.Floor(CurrentOrder.PaymentValue*0.3);
                    }
                case Models.Base.ClinetSubscription.Gold:
                    if (CurrentOrder.CancelDateTime>((DateTime)CurrentOrder.ReserveDateTime).AddMinutes(-15))
                    {
                        return CurrentOrder.PaymentValue;
                    }
                    else
                    {
                        return (int)Math.Floor(CurrentOrder.PaymentValue*0.3);
                    }
                default:
                    return 0;
            }
        }
        public ICommand CancelReserve { get; set; }
        public void SaveComment()
        {
            if (CurrentOrder is not null)
            {
                CurrentOrder.OrderComment??="";
                CurrentOrder.OrderComment = Comment;
                Context.Orders.Update(CurrentOrder);
                Context.SaveChanges();
            }
            
        }
    }
}
