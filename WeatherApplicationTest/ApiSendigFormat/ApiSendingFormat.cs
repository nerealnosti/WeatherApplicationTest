using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApplicationTest.Model;
using WeatherApplicationTest.Model.FiveDay;


namespace WeatherApplicationTest.ApiSendigFormat
{
    class ApiSendingFormat
    {
        //http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=fcBBktwweYkvpzoFAvMn6fgAXUv72c6y&q=belg

        //http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=fcBBktwweYkvpzoFAvMn6fgAXUv72c6y&q=Belg
        //http://dataservice.accuweather.com/currentconditions/v1/298198?apikey=fcBBktwweYkvpzoFAvMn6fgAXUv72c6y&details=true

        private const string Base_url = "http://dataservice.accuweather.com/";
        private const string AutoComplete = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        /*private const string API = "cEqkw1lT8vCQWWFR3nxjEKQdGaDp6oPw";*/
        private const string API = "T6ouI10df3xiOMyjgoAq7GXf3yBsQRbh";
        /*private const string API = "fcBBktwweYkvpzoFAvMn6fgAXUv72c6y";*/
        private const string CurrentCondition_EndPoint = "currentconditions/v1/{0}?apikey={1}&details=true";
        private const string FiveDayForecast_url = "forecasts/v1/daily/5day/{0}?apikey={1}&metric=true";
        public static bool failResponse = false;



        public static async Task<List<City>> GetCitiesAsync(string query)
        {
            List<City> cities = new List<City>();
            string url = Base_url + string.Format(AutoComplete, API, query);



            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                failResponse = !response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    string Json = await response.Content.ReadAsStringAsync();

                    cities = JsonConvert.DeserializeObject<List<City>>(Json);

                    
                }

                
            }

            
            return cities;
        }


        public static async Task<CurrentCondition> CurrentConditionsAsync (string cityKey)
        {
            CurrentCondition current = new CurrentCondition();
            string url = Base_url + string.Format(CurrentCondition_EndPoint, cityKey, API);

            using(HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var currentt= CurrentCondition.FromJson(json);
                current = (CurrentCondition)currentt.First();

            }
            
            
            return current;
        }

        public static async Task<List<DailyForecast>> FiveDays (string cityKey)
        {
            FiveDayForecast fiveDayForecasts = new FiveDayForecast();
            List<DailyForecast> dailyForecasts = new List<DailyForecast>();
            string url = Base_url + string.Format(FiveDayForecast_url, cityKey, API);
            //http://dataservice.accuweather.com/forecasts/v1/daily/5day/298198?apikey=T6ouI10df3xiOMyjgoAq7GXf3yBsQRbh&metric=true
            //http://dataservice.accuweather.com/forecasts/v1/daily/5day/332283?apikey=T6ouI10df3xiOMyjgoAq7GXf3yBsQRbh&metric=true"

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var Json = await response.Content.ReadAsStringAsync();
                    
                    fiveDayForecasts = JsonConvert.DeserializeObject<FiveDayForecast>(Json);
                    /*fiveDayForecasts = JsonConvert.DeserializeObject<List<FiveDayForecast>>(Json);*/

                }
            }

            dailyForecasts = (List<DailyForecast>) fiveDayForecasts.DailyForecasts;
            dailyForecasts.Remove(dailyForecasts.First());
            dailyForecasts.Remove(dailyForecasts.Last());
            return dailyForecasts;
        }

    }
}
