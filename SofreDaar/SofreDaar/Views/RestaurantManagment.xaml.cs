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
    /// Interaction logic for RestaurantManagment.xaml
    /// </summary>
    public partial class RestaurantManagment : UserControl
    {
        public RestaurantManagment()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is RestaurantManagmentViewModel viewModel)
            {
                VM = viewModel;
            }
        }


        public RestaurantManagmentViewModel VM { get; set; }
    }
}
