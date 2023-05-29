using System.Windows.Controls;

namespace SistemaLibreriaImagina.Utilities
{
    public class ErrorControl : UserControl
    {
        public string ErrorMessage { get; set; }

        public ErrorControl()
        {
            // Configurar el contenido del control de error
            Content = new TextBlock
            {
                Text = ErrorMessage,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };
        }
    }
}