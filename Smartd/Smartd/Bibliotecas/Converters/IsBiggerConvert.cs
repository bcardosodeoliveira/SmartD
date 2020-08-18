using System;
using System.Globalization;
using Xamarin.Forms;

namespace Smartd.Bibliotecas.Converters
{
    public class IsBiggerConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value.ToInt() > parameter.ToInt();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
