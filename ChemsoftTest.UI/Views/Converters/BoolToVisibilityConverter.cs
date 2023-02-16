using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ChemsoftTest.UI.Views.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = (bool)value!;
        var visibility = (bool)parameter!;
        return val
            ? (visibility ? Visibility.Visible : Visibility.Hidden)
            : (visibility ? Visibility.Hidden : Visibility.Visible);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}