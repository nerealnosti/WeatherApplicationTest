using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApplicationTest.ViewModel.Commands
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            double.TryParse(value.ToString(), out double s);
            double.TryParse(parameter.ToString(), out double d);

            if (d!=0)
            {
                
                return s * d; 
            }
            else
            {
                return d;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "not posible";
        }
    }
}
