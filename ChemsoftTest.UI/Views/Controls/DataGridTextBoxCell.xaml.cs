using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChemsoftTest.UI.Views.Controls;

public partial class DataGridTextBoxCell
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(DataGridTextBoxCell));

    public static readonly DependencyProperty CellBackgroundProperty =
        DependencyProperty.Register(
            nameof(CellBackground),
            typeof(Color),
            typeof(DataGridTextBoxCell));
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color CellBackground
    {
        get => (Color)GetValue(CellBackgroundProperty);
        set => SetValue(CellBackgroundProperty, value);
    }
    
    public DataGridTextBoxCell()
    {
        InitializeComponent();
    }
}