using System;
using System.Collections.Generic;

namespace Challenge_WirTrack.Models.Dtos
{
    public class WeatherInfoDto
    {

        public class daily
        {
            public List<Weather> weather { get; set; }

        }

        public class Weather
        {
            public string main { get; set; }
        }

        public class root
        {
            public List<Weather> weather { get; set; }
            public List<daily> daily { get; set; }
        }

    }
}
