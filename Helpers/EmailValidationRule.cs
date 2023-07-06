using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.Helpers
{
    public class EmailValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult(false, ErrorMessage);
            }

            // Utilizar una expresión regular para validar el formato del correo electrónico
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(email))
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return ValidationResult.ValidResult;
        }
    }
}