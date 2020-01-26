using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WeatherApplicationTest.Model.FiveDay;
using WeatherApplicationTest.ViewModel;
using WeatherApplicationTest.ApiSendigFormat;
using WeatherApplicationTest.ViewModel.Commands;
using WeatherApplicationTest.UC;
using System.Collections.ObjectModel;
using WeatherApplicationTest.UControl;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlTypes;

namespace WeatherApplicationTest.ViewModel
{
    
    public class WeaterAppInput : DependencyObject, INotifyPropertyChanged
    {
        
        public int CHG
        {
            get { return (int)GetValue(CHGProperty); }
            set { SetValue(CHGProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CHG.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CHGProperty =
            DependencyProperty.Register("CHG", typeof(int), typeof(WeaterAppInput), new PropertyMetadata(0));




        private string _ip;

        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged(nameof(IP));
            }

        }



        private bool citiesEmpty = true;

        public bool CitiesEmpty
        {
            get { return citiesEmpty; }
            set
            {
                citiesEmpty = value;
                OnPropertyChanged(nameof(CitiesEmpty));
            }
        }


        private bool noResultSearch;

        public bool NoResultSearch
        {
            get { return noResultSearch; }
            set
            {
                noResultSearch = value;
                OnPropertyChanged(nameof(NoResultSearch));
                
            }
        }


        private bool noResponse;

        public bool NoResponse
        {
            get { return noResponse; }
            set
            {
                noResponse = value;
                OnPropertyChanged(nameof(NoResponse));
                
            }

        }


        static bool trut;


        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<DailyForecast> Dailies{get; set;}
        


        private string query;
        public  string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }


        private CurrentCondition currentCondition;

        public CurrentCondition CurrentCondition
        {
            get { return currentCondition; }
            set 
            { 
                currentCondition = value;

                OnPropertyChanged(nameof(CurrentCondition));
               
            }
        }



        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {

                if (value!=null)
                {
                    selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                    GetCurrentConditionAsync();
                    GetFiveDayForecast();
                    
                    CitiesEmpty = true;
                }


            }
        }



        private ActualSize widthAct;

        public ActualSize WidthAct
        {
            get
            { return widthAct; }
            set
            {
                widthAct = value;
                OnPropertyChanged(nameof(WidthAct));
            }

        }


        


        private DailyForecast daily;

        public DailyForecast Daily
        {
            get { return daily; }
            set
            {
                daily = value;
                OnPropertyChanged(nameof(Daily));
            }

        }

        public static WeaterAppInput WeaterApp { get; set; }



        public Command Command { get; set; }
        public PressureTandConverter PressureTandConverter { get; set; }

        

        public event PropertyChangedEventHandler PropertyChanged;



        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        public WeaterAppInput()
        {
            if (!trut)
            {
                WeaterApp = this;
                trut = true;
            }
            Application.Current.MainWindow.Closing += MainWindow_Closing;
            Application.Current.MainWindow.Loaded += MainWindow_Loaded;
            Cities = new ObservableCollection<City>();
            Dailies = new ObservableCollection<DailyForecast>();
            Command = new Command(WeaterApp);
            WidthAct = WeaterHomePageWindow.Act;
            CitiesEmpty = true;

            if (!File.Exists(@"..\..\Serialzation\SelectedCity.dat"))
            {

                IP = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                GetCurrentConditionIPAsync();

            }


        }

        

        public async Task MakeQuerzAsync()
        {
            ApiSendingFormat.failResponse = false;
            NoResultSearch = false;
            NoResponse = false;


            var cities = await ApiSendigFormat.ApiSendingFormat.GetCitiesAsync(Query);
            

            if (cities.Count != 0)
            {
                if (Cities.Count > 0)
                {
                    Cities.Clear();

                }

                foreach (var item in cities)
                {
                    Cities.Add(item);
                }
                
                CitiesEmpty = false;

                return;
            }

            if (ApiSendingFormat.failResponse)
            {
                NoResponse = true;
                return;
            }
            if (cities.Count == 0)
            {
                NoResultSearch = true;
            }
               
            
            

            
        }


        private async Task GetCurrentConditionIPAsync()
        {
            SelectedCity = await ApiSendingFormat.IPCityName();
            WeaterHomePageWindow.Act.Width = (1 - 1);
            
        }

        private async Task GetCurrentConditionAsync()
        {
            Query = string.Empty;

            if (Cities.Count > 0)
            {
                Cities.Clear();

            }
            CitiesEmpty = true;
            CurrentCondition = await ApiSendingFormat.CurrentConditionsAsync(SelectedCity.Key);
            /*await Task.Delay(TimeSpan.FromSeconds(2));*/
            WeaterHomePageWindow.Act.Width = (1 - 1);
            
        }


        private async Task GetFiveDayForecast()
        {
            Dailies?.Clear();
            var Daily1 = await ApiSendingFormat.FiveDays(selectedCity.Key);

            foreach (var item in Daily1)
            {
                Dailies.Add(item);

            }

            
        }

        #region MainLoad and Close Serialization
        /// <summary>
        /// On main window closing serialization City
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (SelectedCity != null)
            {
                Serialization(SelectedCity);
            }
        }

        /// <summary>
        /// On window loaded deserialzation city
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            if (File.Exists(@"..\..\Serialzation\SelectedCity.dat"))
            {
                using (Stream stream = File.Open(@"..\..\Serialzation\SelectedCity.dat", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    SelectedCity = (City)bin.Deserialize(stream);
                } 
            } 
            
        } 

        #endregion


        public static void Serialization(City city)
        {
            using(Stream stream = File.Open(@"..\..\Serialzation\SelectedCity.dat", FileMode.OpenOrCreate))
            {

                BinaryFormatter bin = new BinaryFormatter();

                bin.Serialize(stream, city);


            }
        }
    }

    
}
