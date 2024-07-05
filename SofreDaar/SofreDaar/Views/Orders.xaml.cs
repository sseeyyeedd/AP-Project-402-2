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
    public partial class Orders : UserControl
    {
        private object lastselected;
        public Orders()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is OrdersViewModel viewModel)
            {
                VM = viewModel;
            }
        }

        public OrdersViewModel VM { get; set; }

        private void MenuListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastselected=MenuListView.SelectedItem;
        }

        private void MenuListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MenuListView.SelectedItem==lastselected)
            {
                MenuListView.SelectedItem=null;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            VM.SaveComment();
        }
    }
}
