using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApplicationTest.ViewModel.Commands
{
    public class WindSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse(value.ToString(), out double S);
            double.TryParse(parameter.ToString(), out double D);

            double X = S * D;

            if (X<-90)
            {
                X = -90;
                return X;
            }
            else
            {
                return X;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "No";
        }
    }
}
