using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WeatherApplicationTest;

namespace WeatherApplicationTest.ViewModel.Converter
{
    public class DayNightTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                SolidColorBrush nullValue = new SolidColorBrush();
                nullValue.Color = (Color)ColorConverter.ConvertFromString("#116B8F");
                return nullValue;
            }
            if ((bool)value)
            {
                LinearGradientBrush mainDayTime = new LinearGradientBrush();
                GradientStop gradientStopOne = new GradientStop();
                gradientStopOne.Color = (Color)ColorConverter.ConvertFromString("#00A5A7");
                gradientStopOne.Offset = 0.3;

                mainDayTime.GradientStops.Add(gradientStopOne);

                GradientStop gradientStopTwo = new GradientStop();
                gradientStopTwo.Color = (Color)ColorConverter.ConvertFromString("#009092");
                gradientStopTwo.Offset = 0.5;

                mainDayTime.GradientStops.Add(gradientStopTwo);

                GradientStop gradientStopTree = new GradientStop();
                gradientStopTree.Color = (Color)ColorConverter.ConvertFromString("#007476");
                gradientStopTree.Offset = 0.7;

                mainDayTime.GradientStops.Add(gradientStopTree);


                GradientStop gradientStopFour = new GradientStop();
                gradientStopFour.Color = (Color)ColorConverter.ConvertFromString("#003D3D");
                gradientStopFour.Offset = 1;

                
                mainDayTime.GradientStops.Add(gradientStopFour);


                return mainDayTime;

            }
            else
            {
                LinearGradientBrush mainNightTime = new LinearGradientBrush();
                GradientStop gradientStopOne = new GradientStop();
                gradientStopOne.Color = (Color)ColorConverter.ConvertFromString("#444B69");
                gradientStopOne.Offset = 0.3;

                mainNightTime.GradientStops.Add(gradientStopOne);

                GradientStop gradientStopTwo = new GradientStop();
                gradientStopTwo.Color = (Color)ColorConverter.ConvertFromString("#30324D");
                gradientStopTwo.Offset = 0.5;

                mainNightTime.GradientStops.Add(gradientStopTwo);

                GradientStop gradientStopTree = new GradientStop();
                gradientStopTree.Color = (Color)ColorConverter.ConvertFromString("#0A0B13");
                gradientStopTree.Offset = 0.8;

                mainNightTime.GradientStops.Add(gradientStopTree);


                GradientStop gradientStopFour = new GradientStop();
                gradientStopFour.Color = (Color)ColorConverter.ConvertFromString("#000000");
                gradientStopFour.Offset = 1;

                mainNightTime.GradientStops.Add(gradientStopFour);

                return mainNightTime;
            }

           



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "NO";
        }
    }
}
