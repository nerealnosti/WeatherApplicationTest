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


namespace WeatherApplicationTest.ViewModel
{
    
    public class WeaterAppInput : DependencyObject, INotifyPropertyChanged
    {
        public static string KeySelected = "";
        public int CHG
        {
            get { return (int)GetValue(CHGProperty); }
            set { SetValue(CHGProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CHG.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CHGProperty =
            DependencyProperty.Register("CHG", typeof(int), typeof(WeaterAppInput), new PropertyMetadata(0));







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
                    KeySelected =  selectedCity.Key;
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
            Cities = new ObservableCollection<City>();
            Dailies = new ObservableCollection<DailyForecast>();
            Command = new Command(WeaterApp);
            WidthAct = WeaterHomePageWindow.Act;
            CitiesEmpty = true;
            CurrentCondition = new CurrentCondition
            {
                Temperature = new Model.Temperature()
                {
                    Metric = new Metric
                    {
                        Value = -22.2
                    }
                },

                RealFeelTemperature = new ApparentTemperature()
                {
                    Metric = new Metric
                    {
                        Value = 23
                    }
                },

                WeatherText = "Sunny",

                Wind = new Wind
                {
                    Direction = new Direction()
                    {
                        Degrees = 132,
                        Localized = "SEE"
                    },
                    Speed = new ApparentTemperature()
                    {
                        Metric = new Metric
                        {
                            Value = 132,
                            Unit = "km/h"
                        }
                    },

                },
                Pressure = new ApparentTemperature()
                {
                    Metric = new Metric
                    {
                        Value = 950
                    }
                },
                WeatherIcon = 16
                    
                    
                
                    
                };

            

                
            }

        

        public async Task MakeQuerzAsync()
        {
            ApiSendingFormat.failResponse = false;
            NoResultSearch = false;
            NoResponse = false;
            
            var cities = await ApiSendigFormat.ApiSendingFormat.GetCitiesAsync(Query);
            

            if (cities.Count !=0)
            {
                if (Cities.Count > 0)
                {
                    Cities.Clear();

                }

                foreach (var item in cities)
                {
                    Cities.Add(item);
                }
                Task.WaitAny();
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

        private async Task GetCurrentConditionAsync()
        {
            Query = string.Empty;

            if (Cities.Count > 0)
            {
                Cities.Clear();

            }
            CitiesEmpty = true;
            CurrentCondition = await ApiSendingFormat.CurrentConditionsAsync(SelectedCity.Key);
            Task.Delay(TimeSpan.FromSeconds(2));
            WeaterHomePageWindow.Act.Width = (1 - 1);
            
        }


        private async Task GetFiveDayForecast()
        {
            Dailies?.Clear();
            var Daily1 = await ApiSendingFormat.FiveDays(KeySelected);

            foreach (var item in Daily1)
            {
                Dailies.Add(item);

            }
        }

       
        
    }

    
}
