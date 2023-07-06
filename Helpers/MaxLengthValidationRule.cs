using System.Globalization;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.Helpers
{
    public class MaxLengthValidationRule : ValidationRule
    {
        public int MaxLength { get; set; }
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str != null && str.Length > MaxLength)
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return ValidationResult.ValidResult;
        }
    }

}
