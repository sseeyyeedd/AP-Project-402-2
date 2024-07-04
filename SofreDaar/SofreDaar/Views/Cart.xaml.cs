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
            if (DataContext is RestaurantManagmentViewModel viewModel)
            {
                VM = viewModel;
            }
        }


        public RestaurantManagmentViewModel VM { get; set; }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedItem = e.Row.Item as Restaurant;
                if (editedItem != null)
                {
                    var textBox = e.EditingElement as TextBox;
                    if (textBox != null)
                    {
                        var newValue = textBox.Text;
                        editedItem.Password = newValue;

                        VM.UpdatePassword(editedItem);
                    }
                }
            }
        }

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
