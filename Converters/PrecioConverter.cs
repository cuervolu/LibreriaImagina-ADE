using System;
using System.Globalization;
using System.Windows.Data;

namespace SistemaLibreriaImagina.Converters
{
    public class PrecioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal precio)
            {
                CultureInfo clpCulture = new CultureInfo("es-CL");
                return precio.ToString("C", clpCulture);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
