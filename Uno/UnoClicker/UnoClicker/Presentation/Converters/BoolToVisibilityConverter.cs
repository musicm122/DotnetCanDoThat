using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoClicker.Presentation.Converters
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((value is bool && (bool)value))
        {
            return Visibility.Visible;
        }
        else
        {
            return Visibility.Collapsed;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility && (Visibility)value == Visibility.Visible;
    }
}
