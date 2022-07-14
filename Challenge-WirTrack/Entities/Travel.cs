using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_WirTrack.Entities
{
    public class Travel : BaseEntity
    {
        public DateTime Date { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
