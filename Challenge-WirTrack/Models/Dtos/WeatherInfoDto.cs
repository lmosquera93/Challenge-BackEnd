using System;
using System.Collections.Generic;

namespace Challenge_WirTrack.Models.Dtos
{
    public class WeatherInfoDto
    {
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
        }

        public class list
        {
            public List<Weather> weather { get; set; }
            public string dt_txt { get; set; }
        }
    }
}
