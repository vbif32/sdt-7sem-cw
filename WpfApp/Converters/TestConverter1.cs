using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Windows.Controls;
using WpfApp.EntitiesVM;

namespace WpfApp.Converters
{
    class TestConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (value as DataGridRow)?.DataContext as SubjectVM;
            return val?.ActualLoadSum < val?.PlannedLoadSum;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
