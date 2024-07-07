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
using SofreDaar.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
namespace SofreDaar.Views
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is SignUpViewModel viewModel)
            {
                VM = viewModel;
            }
        }

        public SignUpViewModel VM { get; set; }

        private void FemaleButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Gender=Models.Base.Gender.Female;
            MaleButton.Foreground=Brushes.White;
            FemaleButton.Foreground=Brushes.Yellow;

        }
        private void MaleButton_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
             var bc1=new BrushConverter();
            VM.Gender=Models.Base.Gender.Male;
            FemaleButton.Foreground=Brushes.White;
            MaleButton.Foreground=Brushes.Yellow;
            
        }
    }
}
