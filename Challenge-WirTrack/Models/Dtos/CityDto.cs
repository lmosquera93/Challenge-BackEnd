using System.ComponentModel.DataAnnotations;

namespace Challenge_WirTrack.Models.Dtos
{
    public class CityDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; }

    }
}
