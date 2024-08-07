﻿using SofreDaar.Infrastructure;
using SofreDaar.Models;
using SofreDaar.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class MenuManagementViewModel:BaseViewModel
    {
        public MenuManagementViewModel(DatabaseContext DbContext, MainViewModel main,DashboardViewModel dashboard) : base(DbContext, main)
        {
            var categories = Context.Categorys.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id);
            var foods = Context.Foods.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id);
            if (categories is null)
            {
                Categories= [];
            }
            else
            {
                Categories=new ObservableCollection<Category>(categories);
            }
            if (foods is null)
            {
                MenuFoods= [];
            }
            else
            {
                MenuFoods=new ObservableCollection<Food>(foods);
            }
            DeleteFoodCommand =new RelayCommand(o =>
            {
                if (CurrentFood.Restaurant is not null)
                {
                    var Result = System.Windows.MessageBox.Show("حذف شود؟", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Result==MessageBoxResult.Yes)
                    {
                        foreach (var item in Context.Commnets)
                        {
                            if (item.FoodId==CurrentFood.Id)
                            {
                                Context.Commnets.Remove(item);
                            }
                        }
                        foreach (var item in Context.OrderItems)
                        {
                            if (item.FoodId==CurrentFood.Id)
                            {
                                Context.OrderItems.Remove(item);
                            }
                        }
                        Context.Remove(CurrentFood);
                        Context.SaveChanges();
                        MenuFoods.Remove(CurrentFood);
                        ResetCurrentFood();
                    }
                  
                }
            });
            AddOrUpdateCommand=new RelayCommand(o =>
            {
                if (Category is null)
                {
                    //category is required
                    return;
                }
                if (CurrentFood.Restaurant is null)
                {
                    CurrentFood.RestaurantId=MainVM.LoggedInUser.Id;
                    CurrentFood.CategoryId=Category.Id;
                    Context.Foods.Add(CurrentFood);
                    MenuFoods.Add(CurrentFood);
                }
                else
                {
                    Context.Foods.Update(CurrentFood);
                    var foods = Context.Foods.Where(x => x.RestaurantId==MainVM.LoggedInUser.Id);
                    if (foods is null)
                    {
                        MenuFoods= [];
                    }
                    else
                    {
                        MenuFoods=new ObservableCollection<Food>(foods);
                    }


                }
                ResetCurrentFood();

                Context.SaveChanges();
            });
            SelectFileCommand=new RelayCommand(o => {
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Multiselect=false;
                dialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
                if (dialog.ShowDialog()??false)
                {
                    CurrentFood.ImageAddress = dialog.FileName;
                }
                
            });
            CommentsCommand=new RelayCommand(o =>
            {
                if (CurrentFood is not null&&CurrentFood.Restaurant is not null)
                {
                    dashboard.CurrentViewModel=new CommentsViewModel(DbContext, MainVM, CurrentFood, this, dashboard);
                }
            });
        }
        private Food _currentFood;

        public Food CurrentFood
        {
            get { return _currentFood; }
            set { _currentFood = value; OnPropertyChanged();
                if (value is not null&&value.Category is not null&&value.Restaurant is not null)
                {
                    Category=Categories.First(x => x.Id==value.CategoryId);
                }
            }
        }
        private ObservableCollection<Food> _menuFoods;

        public ObservableCollection<Food> MenuFoods
        {
            get { return _menuFoods; }
            set { _menuFoods = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Category> Categories { get; set; }
        private Category _category;

        public Category Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(); }
        }
        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand SelectFileCommand { get; set; }
        public ICommand CommentsCommand { get; set; }
        public ICommand DeleteFoodCommand { get; set; }
        public void ResetCurrentFood()
        {
            CurrentFood=new Models.Food()
             {
                 Id=Guid.NewGuid(),
                 Name=string.Empty,
                 Price=0,
                 Stock=0,
                 ImageAddress=string.Empty,
                 RawMaterials=string.Empty,

             };
        }
    }
}
