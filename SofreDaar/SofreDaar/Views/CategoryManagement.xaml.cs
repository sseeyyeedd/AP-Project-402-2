﻿using SofreDaar.Models;
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
    /// Interaction logic for CategoryManagement.xaml
    /// </summary>
    public partial class CategoryManagement : UserControl
    {
        public CategoryManagement()
        {
            InitializeComponent();
            DataContextChanged+=OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is CategoryManagementViewModel viewModel)
            {
                VM = viewModel;
            }
        }


        public CategoryManagementViewModel VM { get; set; }
        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }

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
    }
}