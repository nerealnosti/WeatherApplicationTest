using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherApplicationTest.UControl
{
    /// <summary>
    /// Interaction logic for FiveDayForecastUserControl.xaml
    /// </summary>
    public partial class FiveDayForecastUserControl : UserControl
    {
        public FiveDayForecastUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }






        public string TextDP
        {
            get { return (string)GetValue(TextDPProperty); }
            set { SetValue(TextDPProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextDP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextDPProperty =
            DependencyProperty.Register("TextDP", typeof(string), typeof(FiveDayForecastUserControl), new PropertyMetadata(""));






        public string SourceDP
        {
            get { return (string)GetValue(SourceDPProperty); }
            set { SetValue(SourceDPProperty, value);}
        }

        // Using a DependencyProperty as the backing store for SourceDP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceDPProperty =
            DependencyProperty.Register("SourceDP", typeof(string), typeof(FiveDayForecastUserControl), new PropertyMetadata(""));






        public int MaxTemp
        {
            get { return (int)GetValue(MaxTempProperty); }
            set { SetValue(MaxTempProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxTemp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxTempProperty =
            DependencyProperty.Register("MaxTemp", typeof(int), typeof(FiveDayForecastUserControl), new PropertyMetadata(0));



        public int MinTemp
        {
            get { return (int)GetValue(MinTempProperty); }
            set { SetValue(MinTempProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinTemp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinTempProperty =
            DependencyProperty.Register("MinTemp", typeof(int), typeof(FiveDayForecastUserControl), new PropertyMetadata(0));






    }
}
