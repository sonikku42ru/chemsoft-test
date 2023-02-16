﻿using System.Windows;

namespace ChemsoftTest.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;
        
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadPeopleCommand?.Execute(null);
        }
    }
}