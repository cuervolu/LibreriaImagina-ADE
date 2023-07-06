using System.Globalization;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.Helpers
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return ValidationResult.ValidResult;
        }
    }
}
