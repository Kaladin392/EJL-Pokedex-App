using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace PokedexApp.Converters
{
    public class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCaught)
            {
                return isCaught ? "pokeball_full.png" : "pokeball_empty.png";
            }
            return "pokeball_empty.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}




