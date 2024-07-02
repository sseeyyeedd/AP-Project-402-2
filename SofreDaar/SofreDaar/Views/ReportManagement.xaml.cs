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
    /// Interaction logic for ReportManagement.xaml
    /// </summary>
    public partial class ReportManagement : UserControl
    {
        public ReportManagement()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ProfileViewModel viewModel)
            {
                VM = viewModel;
            }
        }


        public ProfileViewModel VM { get; set; }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedItem = e.Row.Item as Report;
                if (editedItem != null)
                {
                    var textBox = e.EditingElement as TextBox;
                    if (textBox != null)
                    {
                        var newValue = textBox.Text;
                        editedItem.Answer = newValue;
                        editedItem.IsFollowedUp=true;

                        VM.UpdateReport(editedItem);
                    }
                }
            }
        }
    }
}
