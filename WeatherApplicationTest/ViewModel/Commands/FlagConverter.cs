using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApplicationTest.ViewModel.Commands
{
    class FlagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var CountryName = value.ToString();
            CountryName = CountryName.Replace(' ', '-');

            string flagpath;
            flagpath  = "/Assets/Flags/";
            string extension = ".png";
            var b = string.Concat(flagpath, CountryName,extension);
            return b;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "error";
        }
    }
}
