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
    /// Interaction logic for MenuManagement.xaml
    /// </summary>
    public partial class OrdersManagement : UserControl
    {
        private object lastselected;
        public OrdersManagement()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is OrdersManagementViewModel viewModel)
            {
                VM = viewModel;
            }
        }

        public OrdersManagementViewModel VM { get; set; }

        private void priceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(((TextBox)sender).Text, out value)&&value>-1)
            {
                VM.FromPrice=value;
            }
            else
            {
                VM.FromPrice=value;
                ((TextBox)sender).Text="0";
            }
        }

        private void topriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(((TextBox)sender).Text, out value)&&value>0)
            {
                VM.ToPrice=VM.ToPrice;
            }
            else
            {
                VM.ToPrice=value;
                ((TextBox)sender).Text="0";
            }
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
        
        }
    }
}
