using System;

namespace Challenge_WirTrack.Models.Dtos
{
    public class InsertDto
    {
        public DateTime Date { get; set; }
        public int CityID { get; set; }
        public int VehicleID { get; set; }
    }
}
