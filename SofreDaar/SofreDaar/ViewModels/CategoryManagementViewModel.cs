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
        List<Category> categories;
        public CategoryManagementViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            categories = Context.Categorys.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id).ToList();
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
                        var foods= Context.Foods.ToList();
                        foreach (var food in foods)
                        {
                            if (food.CategoryId==item.Id)
                            {
                                foreach (var comment in Context.Commnets)
                                {
                                    if (comment.FoodId==food.Id)
                                    {
                                        Context.Commnets.Remove(comment);
                                    }
                                }
                                foreach (var orderitem in Context.OrderItems)
                                {
                                    if (orderitem.FoodId==food.Id)
                                    {
                                        Context.OrderItems.Remove(orderitem);
                                    }
                                }
                                Context.Foods.Remove(food);
                            }
                        }
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
                try
                {
                    Context.SaveChanges();
                }
                catch 
                {

                }
               
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
