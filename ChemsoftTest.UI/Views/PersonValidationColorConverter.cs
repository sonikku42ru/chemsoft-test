using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ChemsoftTest.UI.Views.Base;
using ChemsoftTest.UI.Views.Models;

namespace ChemsoftTest.UI.Views;

public class PersonValidationColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Colors.Transparent;
        if (value.ToString() == "{NewItemPlaceholder}")
            return Colors.Transparent;
        var valid = (bool)value;
        return valid ? Colors.Transparent : Colors.Red;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return false;
        var color = (Color)value;
        return color != Colors.Red;
    }
}