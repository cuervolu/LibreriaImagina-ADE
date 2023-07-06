using System.Globalization;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.Helpers
{
    public class ComboBoxValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString() == "Seleccione rol...")
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return ValidationResult.ValidResult;
        }
    }
}
