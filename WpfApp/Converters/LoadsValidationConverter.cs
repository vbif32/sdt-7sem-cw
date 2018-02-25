using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp.Converters
{
    internal class LoadsValidationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Ожидается что
            // values[0] - плановая нагрузка
            // values[1] - реальная нагрузка
            // values[2] - флаг активности предмета

            var i1 = float.TryParse(values[0]?.ToString(), out var plannedLoad);
            var i2 = float.TryParse(values[1]?.ToString(), out var actualLoad);
            if (values.Length > 2)
            {
                var i3 = bool.TryParse(values[2]?.ToString(), out var isActive);
                if (i3 && !isActive)
                    return Brushes.Gray;
            }

            if (!(i1 && i2))
                return Brushes.White;
            if (plannedLoad < actualLoad || plannedLoad != 0 && Math.Abs(actualLoad) < 0.5)
                return Brushes.Red;
            if (Math.Abs(plannedLoad - actualLoad) > 0.1 * plannedLoad)
                return Brushes.Yellow;
            return Brushes.Green;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}