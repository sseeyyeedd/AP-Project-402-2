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
    public class CommentsViewModel : BaseViewModel
    {
        public CommentsViewModel(DatabaseContext DbContext, MainViewModel main, Food food, BaseViewModel self, DashboardViewModel dashboard) : base(DbContext, main)
        {
            Dashboard= dashboard;
            Self= self;
            Context.Entry(food).Collection(x => x.Commnets).Load();
            Comments=new ObservableCollection<Commnet>(food.Commnets.Where(x=>x.ReplayToId==null).OrderBy(x=>x.DateTime));
            AddCommentCommand=new RelayCommand(o =>
            {
                var commnet = new Commnet()
                {
                    Id=Guid.NewGuid(),
                    ClientId=self is MenuManagementViewModel?null: MainVM.LoggedInUser.Id,
                    FoodId=food.Id,
                    DateTime=DateTime.Now,
                    isEdited=false,
                    Text=Text,
                    ReplayToId=null
                };
                Context.Commnets.Add(commnet);
                Context.SaveChanges();
                Comments.Add(commnet);
                Text="";
            });
            BackCommand=new RelayCommand(o =>
            {
                if (self is MenuViewModel)
                {
                    dashboard.CurrentViewModel=self as MenuViewModel;
                }
                else if (self is MenuManagementViewModel)
                {
                    dashboard.CurrentViewModel=self as MenuManagementViewModel;
                }
            });

        }
        public DashboardViewModel Dashboard { get; set; }
        public BaseViewModel Self { get; set; }
        private ObservableCollection<Commnet> _comments;

        public ObservableCollection<Commnet> Comments
        {
            get { return _comments; }
            set { _comments = value; OnPropertyChanged(); }
        }
        private Commnet _comment;

        public Commnet Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                if (value is not null)
                {
                    Dashboard.CurrentViewModel=new CommentReplysViewModel(Context, MainVM, this, Comment, Self, Dashboard);
                }
            }
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }
        public ICommand AddCommentCommand { get; set; }
        public ICommand BackCommand { get; set; }


    }
}
