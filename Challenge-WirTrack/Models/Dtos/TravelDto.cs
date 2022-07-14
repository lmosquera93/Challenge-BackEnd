using Challenge_WirTrack.Entities;
using System;

namespace Challenge_WirTrack.Models.Dtos
{
    public class TravelDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
