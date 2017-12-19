using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp.Converters
{
    class LoadsValidationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var i1 = float.TryParse(values[0]?.ToString(), out var val1);
            var i2 = float.TryParse(values[1]?.ToString(), out var val2);

            if (!(i1 && i2))
                return Brushes.White;

            if (val1 < val2)
                return Brushes.Red;
            if (val1 > val2)
                return Brushes.Yellow;
            return Brushes.Green;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
