using System.Windows.Controls;
using ChemsoftTest.UI.Views.Models;

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
    }
}