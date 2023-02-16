using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ChemsoftTest.UI.Views.Modals;

public partial class AlertWindow : Window
{
    private AlertWindow()
    {
        InitializeComponent();
    }

    public static void Show(string title, string message)
    {
        Application.Current.Dispatcher.Invoke(
            DispatcherPriority.Background,
            new ThreadStart(() =>
            {
                var window = new AlertWindow();
                window.Title = title;
                window.Text.Text = message;
                window.OkButton.Content = "OK";
                window.ShowDialog();
            }));
    }

    private void OnOkClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }
}