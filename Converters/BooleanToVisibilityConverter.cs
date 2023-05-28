using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SistemaLibreriaImagina.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        // Convierte un valor booleano en Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLoading && isLoading)
                return Visibility.Visible; // Si el valor es true, se muestra el elemento
            else
                return Visibility.Collapsed; // Si el valor es false, se oculta el elemento
        }

        // No se implementa la conversión inversa
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
