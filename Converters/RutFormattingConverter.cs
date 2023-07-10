using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace SistemaLibreriaImagina.Converters
{
    public class RutFormattingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rut = value as string;

            if (rut != null)
            {
                // Eliminar todos los caracteres no permitidos del RUT
                rut = Regex.Replace(rut, @"[^0-9kK]", "");

                // Formatear el RUT
                if (rut.Length > 1)
                {
                    // Insertar el guion ("-") antes del dígito verificador
                    rut = rut.Insert(rut.Length - 1, "-");

                    // Insertar los puntos (".") en las posiciones adecuadas para formatear el RUT
                    if (rut.Length >= 9)
                    {
                        rut = rut.Insert(rut.Length - 8, ".");
                        rut = rut.Insert(rut.Length - 5, ".");
                    }
                }

                // Ajustar el formato del RUT a "XX.XXX.XXX-X"
                if (rut.Length == 11)
                {
                    rut = rut.Substring(0, 2) + "." + rut.Substring(2, 3) + "." + rut.Substring(5, 3) + "-" + rut.Substring(8, 1);
                }
            }

            return rut;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // El método ConvertBack no es utilizado en este caso,
            // por lo que puedes devolver un valor por defecto.
            return value;
        }
    }
}
