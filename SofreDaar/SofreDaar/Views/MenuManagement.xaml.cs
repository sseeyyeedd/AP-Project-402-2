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
    public partial class MenuManagement : UserControl
    {
        private object lastselected;
        public MenuManagement()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is MenuManagementViewModel viewModel)
            {
                VM = viewModel;
                VM.ResetCurrentFood();
            }
        }

        public MenuManagementViewModel VM { get; set; }

        private void MenuListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastselected=MenuListView.SelectedItem;
        }

        private void MenuListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MenuListView.SelectedItem==lastselected)
            {
                MenuListView.SelectedItem=null;
                VM.ResetCurrentFood();
                VM.Category=null;
                stockTextBox.Text="1";
                priceTextBox.Text="1";
                addAndUpdateButton.Content="افزودن";
            }
            else
            {
                priceTextBox.Text=VM.CurrentFood.Price.ToString();
                stockTextBox.Text=VM.CurrentFood.Stock.ToString();
                addAndUpdateButton.Content="به روزرسانی";
            }
        }

        private void MenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.CurrentFood is null)
            {
                VM.ResetCurrentFood();
            }
        }

        private void priceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 1;
            if (int.TryParse(((TextBox)sender).Text, out value)&&value>0)
            {
                VM.CurrentFood.Price=value;
            }
            else
            {
                VM.CurrentFood.Price=value;
                ((TextBox)sender).Text="1";
            }
        }

        private void stockTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 1;
            if (int.TryParse(((TextBox)sender).Text, out value)&&value>0)
            {
                VM.CurrentFood.Stock=value;
            }
            else
            {
                VM.CurrentFood.Stock=value;
                ((TextBox)sender).Text="1";
            }
        }

        private void addAndUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            VM.AddOrUpdateCommand.Execute(sender);
            VM.Category=null;
            priceTextBox.Text="1";
            stockTextBox.Text="1";
            addAndUpdateButton.Content="افزودن";
            MenuListView.SelectedIndex=-1;
        }
    }
}
