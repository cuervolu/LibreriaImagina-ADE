using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SistemaLibreriaImagina.Converters
{
    public class SelectedProductConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string selectedProductName && parameter is IEnumerable<LIBRO> libros)
            {
                return libros.FirstOrDefault(libro => libro.NOMBRE_LIBRO == selectedProductName);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
