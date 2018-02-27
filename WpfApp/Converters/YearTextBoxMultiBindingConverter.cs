using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Converters
{
    internal class YearTextBoxMultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Ожидается что
            // values[0] - год начала
            // values[1] - год конца

            var i1 = int.TryParse(values[0]?.ToString(), out var startYear);
            var i2 = int.TryParse(values[1]?.ToString(), out var endYear);

            return startYear + "-" + endYear;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}