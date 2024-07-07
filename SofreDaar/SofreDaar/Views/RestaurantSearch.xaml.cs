using SofreDaar.Models;
using SofreDaar.ViewModels;
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
    public partial class RestaurantSearch : UserControl
    {
        public RestaurantSearch()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is RestaurantSearchViewModel viewModel)
            {
                VM = viewModel;
            }
        }


        public RestaurantSearchViewModel VM { get; set; }

       

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            double value = 0;
            if (double.TryParse(((TextBox)sender).Text, out value))
            {
                VM.RatingSearch=value;
            }
            else
            {
                VM.RatingSearch=value;
                ((TextBox)sender).Text="0";
            }
        }
    }
}
