using System;
using System.Globalization;
using Xamarin.Forms;

namespace Smartd.Bibliotecas.Converters
{
    public class IsNotNullConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retorno = value != null;

            if (value is string)
            {
                string strValue = value?.ToString();
                retorno = !strValue.IsNullOrEmpty();
            }

            return retorno;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
