using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WeatherApplicationTest.Model;

namespace WeatherApplicationTest.ViewModel.Commands
{
    public class PressureTandConverter : IValueConverter
    {
        private WeaterAppInput weaterAppInput;

        public WeaterAppInput WeaterAppInput
        {
            get { return weaterAppInput; }
            set { weaterAppInput = value; }
        }


        public PressureTandConverter()
        {
            WeaterAppInput = WeaterAppInput.WeaterApp;
        }
        
        public double Cnvrt(double doub)
        {
            double result =0;
            if (doub>1010)
            {
                doub -= 1000;
                result = (int)doub / 9;
                if (result > 7)
                {
                    result = 7;
                }
                result *= 0.01;

                return result*-1;
            }
            else if (doub<1000)
            {
                doub -= 950;
                result = (int)doub / 8;
                if (result>7)
                {
                    result = 7;
                }
                result += -7;
                result *= -1;
                result *= 0.01;
                return result;
            }
            else
            {
                return 0;
            }
        }

        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double.TryParse(value.ToString(), out double val);
            double.TryParse(WeaterAppInput?.CurrentCondition?.Pressure.Metric.Value.ToString(), out double CC);
            var c = (val*(-0.075 + (Cnvrt(CC))));
            return c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return " ";
        }
    }
}
