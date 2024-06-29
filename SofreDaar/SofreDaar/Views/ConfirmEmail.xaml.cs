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
    /// Interaction logic for ConfirmEmail.xaml
    /// </summary>
    public partial class ConfirmEmail : UserControl
    {
        public ConfirmEmail()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ConfirmEmailViewModel viewModel)
            {
                VM = viewModel;
                emailLabel.Content=(VM.GetEmail()??"");
            }
        }

        public ConfirmEmailViewModel VM { get; set; }
    }
}
