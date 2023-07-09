using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SistemaLibreriaImagina.Converters
{
    public class RolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string rol && parameter is string parametro)
            {
                // Lógica para determinar la visibilidad basada en el rol y el parámetro
                bool isVisible = false;

                if (rol == "Admin")
                {
                    isVisible = true;
                }
                else if (rol == "Repartidor" && parametro == "Pedidos")
                {
                    isVisible = true;
                }
                else if (rol == "Encargado de Bodega" && (parametro == "Inventario" || parametro == "Pedidos" || parametro == "Restock"))
                {
                    isVisible = true;
                }
                else if (rol == "Técnico" && parametro != "Usuarios")
                {
                    isVisible = true;
                }
                else if (rol == "Empleado" && (parametro == "POS" || parametro == "Inventario" || parametro == "Inicio"))
                {
                    isVisible = true;
                }

                return isVisible ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
