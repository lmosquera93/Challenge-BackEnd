using System.ComponentModel.DataAnnotations;

namespace Challenge_WirTrack.Models.Dtos
{
    public class VehicleDto
    {
        [Required(ErrorMessage = "El typo es obligatorio.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "La patente es obligatoria.")]
        public string Patent { get; set; }
        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Brand { get; set; }
    }
}
