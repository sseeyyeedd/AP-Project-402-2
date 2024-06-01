﻿using SofreDaar.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SofreDaar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel VM { get; set; }
        public MainWindow()
        {
            VM = new MainViewModel();
            DataContext=VM;
            
            InitializeComponent();
           
        }
        

    }
}