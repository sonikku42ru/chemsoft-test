using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ChemsoftTest.UI.Views.Modals;

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
            viewModel.CloseRequested += (_, _) => Application.Current.Dispatcher.Invoke(
                DispatcherPriority.Background,
                new ThreadStart(Close));
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadPeopleCommand?.Execute(null);
        }
    }
}