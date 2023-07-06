using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.Helpers
{
    public class PhoneValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var phoneNumber = value as string;

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                // Expresión regular para validar el formato del número de teléfono
                var regex = new Regex(@"^\+\d{1,3}\s?\(\d{1,3}\)\s?\d{3}-\d{4}$");

                if (!regex.IsMatch(phoneNumber))
                {
                    return new ValidationResult(false, ErrorMessage);
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
