using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AppStudio.Controls
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;
            if (value is bool && (bool)value)
            {
                visibility = Visibility.Visible;
            }

            bool invertResult;
            if (parameter != null && bool.TryParse(parameter.ToString(), out invertResult))
            {
                if (invertResult)
                {
                    if (visibility == Visibility.Visible)
                    {
                        visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        visibility = Visibility.Visible;
                    }
                }
            }

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
