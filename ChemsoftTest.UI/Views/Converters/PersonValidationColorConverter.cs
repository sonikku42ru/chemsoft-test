using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ChemsoftTest.UI.Views.Converters;

public class PersonValidationColorConverter : IValueConverter
{
    private static readonly Color ErrorColor = Color.FromRgb(255, 51, 92);
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Colors.White;
        if (value.ToString() == "{NewItemPlaceholder}")
            return Colors.White;
        var valid = (bool)value;
        return valid ? Colors.White : ErrorColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return false;
        var color = (Color)value;
        return color != ErrorColor;
    }
}