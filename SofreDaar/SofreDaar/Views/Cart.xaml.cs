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
    public partial class Cart : UserControl
    {
        public Cart()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is CartManagmentViewModel viewModel)
            {
                VM = viewModel;
            }
        }


        public CartManagmentViewModel VM { get; set; }


      

        private void minutesTextBod_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(((TextBox)sender).Text, out value)&&value>0&&value<60)
            {
                VM.Minutes=value;
            }
            else
            {
                VM.Minutes=value;
                ((TextBox)sender).Text="0";
            }
        }

        private void hoursTextBod_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value =0;
            if (int.TryParse(((TextBox)sender).Text, out value)&&value>0&&value<24)
            {
                VM.Hours=value;
            }
            else
            {
                VM.Hours=value;
                ((TextBox)sender).Text="0";
            }
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            ((DatePicker)sender).BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today));
        }
    }
}
