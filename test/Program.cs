using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            GetRequest("http://api.namnapi.se/v2/names.json?limit=20");
            //GetRequest("http://api.openweathermap.org/data/2.5/weather?lat=35&lon=139&APPID=ca3be28fc38368253303b390611bf4da");
        }

        async static void GetRequest(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            HttpContent content = response.Content;

            string data = await content.ReadAsStringAsync();

            dynamic d = JObject.Parse(data);
            var main = d.weather[0].main.ToString();
            var description = d.weather[0].description.ToString();

            Console.WriteLine(main);
            Console.WriteLine(description);

            //var weatherData = new List<Weather>
            //{
            //    new Weather { Main = main, Description = description }
            //};


            //Console.WriteLine(weatherData);

            //Json.DeserializeObject<Weather>(data)

            Console.ReadLine();
        }

        //async static Task<List<Weather>> GetWeather(string url)
        //{

        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);

        //    HttpContent content = response.Content;

        //    string data = await content.ReadAsStringAsync();

        //    dynamic d = JObject.Parse(data);
        //    var main = d.weather[0].main.ToString();
        //    var description = d.weather[0].description.ToString();

        //    var weatherData = new List<Weather>
        //    {
        //        new Weather { Main = main,
        //                      Description = description }
        //    };

        //    //Console.WriteLine(weatherData);

        //    return weatherData;
        //}
    }
}
