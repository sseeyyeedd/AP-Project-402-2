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
    public class CategoryManagementViewModel:BaseViewModel
    {
        IEnumerable<Category> categories;
        public CategoryManagementViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            categories = Context.Categorys.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id);
            if (categories is null)
            {
                Categories = [];
            }
            else
            {
                Categories=new ObservableCollection<Category>(categories);
            }
            SaveDataCommand= new RelayCommand(o =>
            {
                foreach (var item in categories)
                {
                    if (!Categories.Contains(item))
                    {
                        Context.Categorys.Remove(item);
                    }
                }
                foreach (var item in Categories)
                {
                    if (categories.Any(x=>x.Id==item.Id))
                    {
                        Context.Categorys.Update(item);
                    }
                    else
                    {
                        item.RestaurantId=MainVM.LoggedInUser.Id;
                        item.Id=Guid.NewGuid();
                        Context.Categorys.Add(item);
                    }
                }
                Context.SaveChanges();
            });
        }
        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }

        public ICommand SaveDataCommand { get; set; }
    }
}
