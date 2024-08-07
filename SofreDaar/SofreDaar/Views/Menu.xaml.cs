﻿using SofreDaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SofreDaar.Views
{
    /// <summary>
    /// Interaction logic for MenuManagement.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        private object lastselected;
        public Menu()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is MenuViewModel viewModel)
            {
                VM = viewModel;
            }
        }

        public MenuViewModel VM { get; set; }

        private void MenuListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastselected=MenuListView.SelectedItem;
        }

        

        
    }
}
