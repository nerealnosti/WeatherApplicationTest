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
        //https://api.ipdata.co/178.223.19.37/region?api-key=3f5242c9665b47f644470013463a1fd4f5ba839604d7de547574a9bd
        //"http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey=T6ouI10df3xiOMyjgoAq7GXf3yBsQRbh&q=44.3853%2C20.2557&toplevel=true"
        //"http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey=T6ouI10df3xiOMyjgoAq7GXf3yBsQRbh&q=44.3853%2C20.2557&toplevel=true"

        private const string Base_urlCityLoad = "http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey=";
        private const string AutoCompleteCityLoad = "{0}&q={1}%2C{2}&toplevel=true";
        public static string Lat = string.Empty;
        public static string Lon = string.Empty;


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


        public static async Task<City> IPCityName()
        {
            string CityIPName = string.Empty;
            City IPCity = new City();
            var t  = IpPublicKnowledge.IPK.GetIpInfo(IpPublicKnowledge.IPK.GetMyPublicIp());
            Lat = t.lat;
            Lon = t.lon;


            CityIPName = Base_urlCityLoad + string.Format(AutoCompleteCityLoad, API, Lat, Lon);
            using(HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(CityIPName);
                if (response.IsSuccessStatusCode)
                {
                    var Json = await response.Content.ReadAsStringAsync();

                    IPCity = JsonConvert.DeserializeObject<City>(Json);
                }

            }

            return IPCity;
        }

    }
}
