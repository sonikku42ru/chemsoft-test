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
        var field = (UiField)value;
        if (field == null)
            return Colors.Transparent;
        return field.Valid 
            ? Colors.Black 
            : Colors.Red;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}