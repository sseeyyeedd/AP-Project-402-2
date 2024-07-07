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
    public class CommentReplysViewModel : BaseViewModel
    {
        public CommentReplysViewModel(DatabaseContext DbContext, MainViewModel main,CommentsViewModel viewModel, Commnet comment, BaseViewModel self, DashboardViewModel dashboard) : base(DbContext, main)
        {
            
            Comment = comment;
            var rate = Context.Ratings.FirstOrDefault(x => x.FoodId==comment.FoodId&&x.ClientId==comment.ClientId);
            Star=rate is null?0:rate.Star;
            ShowStars=rate is null||comment.ClientId is null;
            MainComment=comment.Text;
            CanEdit=!(self is MenuManagementViewModel||MainVM.LoggedInUser.Id==comment.ClientId);
            CanEdit1=(self is MenuManagementViewModel||MainVM.LoggedInUser.Id==comment.ClientId);
            Context.Entry(comment).Collection(x => x.Replays).Load();
            Comments=new ObservableCollection<Commnet>(comment.Replays.OrderBy(x=>x.DateTime));
            AddCommentCommand=new RelayCommand(o =>
            {
                var commnet = new Commnet()
                {
                    Id=Guid.NewGuid(),
                    ClientId=self is MenuManagementViewModel?null: MainVM.LoggedInUser.Id,
                    FoodId=null,
                    DateTime=DateTime.Now,
                    isEdited=false,
                    Text=Text,
                    ReplayToId=comment.Id
                };
                Context.Commnets.Add(commnet);
                Context.SaveChanges();
                Comments.Add(commnet);
                Text="";
            });
            BackCommand=new RelayCommand(o =>
            {
                dashboard.CurrentViewModel=viewModel;
            });
            RemoveCommentCommand=new RelayCommand(o =>
            {
                foreach (var item in comment.Replays)
                {
                    Context.Commnets.Remove(item);
                }
                var food =Context.Foods.Where(x => x.Id==comment.FoodId).FirstOrDefault();
                Context.Remove(comment);
                Context.SaveChanges();
                dashboard.CurrentViewModel=new CommentsViewModel(DbContext, MainVM, food, self, dashboard);
            });

        }
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
            }
        }
        private string _mainComment;

        public string MainComment
        {
            get { return _mainComment; }
            set
            {
                _mainComment = value;
               OnPropertyChanged();
               Comment.Text= _mainComment;
                Comment.isEdited=true;
                Context.Commnets.Update(Comment);
                Context.SaveChanges();
            }
        }

        private int star;

        public int Star
        {
            get { return star; }
            set
            {
                star = value;
                OnPropertyChanged();
               
            }
        }
        private bool _canEdit;

        public bool CanEdit
        {
            get { return _canEdit; }
            set
            {
                _canEdit = value;
                OnPropertyChanged();
            }
        }
        private bool _canEdit1;

        public bool CanEdit1
        {
            get { return _canEdit1; }
            set
            {
                _canEdit1 = value;
                OnPropertyChanged();
            }
        }
        private bool _showStars;

        public bool ShowStars
        {
            get { return _showStars; }
            set
            {
                _showStars = value;
                OnPropertyChanged();
            }
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }
        public ICommand AddCommentCommand { get; set; }
        public ICommand RemoveCommentCommand { get; set; }
        public ICommand BackCommand { get; set; }


    }
}
