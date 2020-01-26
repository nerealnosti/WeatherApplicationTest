using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Shapes;
using WeatherApplicationTest.Model;
using WeatherApplicationTest.UC;
using WeatherApplicationTest.UControl.Model;
using WeatherApplicationTest.ViewModel;

namespace WeatherApplicationTest
{
    /// <summary>
    /// Interaction logic for WeaterHomePageWindow.xaml
    /// </summary>
    /// 
    public partial class WeaterHomePageWindow : Window , INotifyPropertyChanged
    {

        public static ActualSize Act;
        private ActualSize actualTempStackSize;

        public ActualSize ActualTempStackSize
        {
            get
            {
                return actualTempStackSize;
            }
            set
            {
                
                actualTempStackSize = value;
                OnPropertyChange(nameof(ActualTempStackSize));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }




        public WeaterHomePageWindow()
        {
            ActualTempStackSize = new ActualSize();
            Act = ActualTempStackSize;
            InitializeComponent();
            Stack.Focus();
            
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

       

        private void MainWindowGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Stack.Focus();
            
        }

        private void Stack_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            ActualTempStackSize.Height = Stack.ActualHeight;
            ActualTempStackSize.Width = Stack.ActualWidth;
            Act.WidthProp = Stack.ActualWidth;
            Act.Height = Stack.ActualHeight;
        }

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove(); 
            }
        }

        
    }

    public class ActualSize:DependencyObject, INotifyPropertyChanged
    {

        private double widthProp;

        public double WidthProp
        {
            get { return widthProp; }
            set
            {
                widthProp = value;
                OnPropertyChange(nameof(WidthProp));
            }

        }




        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(ActualSize), new PropertyMetadata(0.0));




        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(ActualSize), new PropertyMetadata(0.0));



        public string FlagPath
        {
            get { return (string)GetValue(FlagPathProperty); }
            set { SetValue(FlagPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FlagPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlagPathProperty =
            DependencyProperty.Register("FlagPath", typeof(string), typeof(ActualSize), new PropertyMetadata("/Assets/Flags/"));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}

