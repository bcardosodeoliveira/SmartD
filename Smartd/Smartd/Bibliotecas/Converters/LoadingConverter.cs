using System;
using System.Globalization;
using Xamarin.Forms;

namespace Smartd.Bibliotecas.Converters
{
    public class LoadingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 0.4d : 1d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
