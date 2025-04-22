using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace PokedexApp.Converters
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string text)
            {
                var texts = text.Split(';');
                return boolValue ? texts[0] : texts[1];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

