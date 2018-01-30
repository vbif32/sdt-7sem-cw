using System.Globalization;
using System.Windows.Controls;

namespace WpfApp.ValidationRules
{
    public class HoursRule : ValidationRule
    {
        public int Min { get; set; } = 0;

        public int Max { get; set; } = int.MaxValue;


        public override ValidationResult Validate(object value, CultureInfo ci)
        {
            int price;
            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                price = int.Parse((string) value);
            }
            catch
            {
                return new ValidationResult(false, "Недопустимые символы.");
            }
            if (price < Min || price > Max)
                return new ValidationResult(false,
                    "Часы не входит в диапазон " + Min + " до " + Max + ".");
            return new ValidationResult(true, null);
        }
    }
}