using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApplicationTest.ViewModel.Converter
{
    class WeaterIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = @"/Assets/IconWeather/";

            if (value == null)
            {
                string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                return null;
            }
           var tempValue = (int)value;

            switch (tempValue)
            {
                case 1:
                    return $"{path}Sunny.png";

                case 2:
                    return $"{path}Mostly-Sunny.png";

                case 3:
                case 4:
                    return $"{path}Partly-Sunny.png";

                case 5:
                    return $"{path}Hazy-Sunshine.png";

                case 6:
                    return $"{path}Mostly-Cloudy.png";

                case 7:
                    return $"{path}Cloudy.png";

                case 8:
                    return $"{path}Dreary.png";

                case 9:
                    return $"{path}Fog.png";

                case 10:
                    return $"{path}Showers.png";

                case 11:
                    return $"{path}Mostly-Cloudy-Showers.png";

                case 12:
                    return $"{path}Partly-Sunny-Showers.png";

                case 13:
                    return $"{path}T-Storm.png";

                case 14:
                    return $"{path}Mostly-Cloudy-T-Storms.png";

                case 15:
                    return $"{path}Partly-Sunny-T-Storms.png";

                case 16:
                    return $"{path}Rain.png";

                default:
                    return "/Assets/IconWeather/Mostly-Sunny.png";
                    
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
