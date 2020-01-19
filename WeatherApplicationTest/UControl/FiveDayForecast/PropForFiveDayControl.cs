using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherApplicationTest.UControl.FiveDayForecast
{
    public class PropForFiveDayControl:DependencyObject,INotifyPropertyChanged
    {


        public string TexDProp
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TextProp", typeof(string), typeof(PropForFiveDayControl), new PropertyMetadata(""));

        private string textTextblock;

        public string TextTextblock
        {
            get { return textTextblock; }
            set
            {
                textTextblock = value;
                OnPropertyChanged(nameof(TextTextblock));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
