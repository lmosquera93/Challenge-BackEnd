using Challenge_WirTrack.Entities;
using Challenge_WirTrack.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge_WirTrack.Models.Mapper
{
    public class CitiesMapper
    {
        public static List<CityDto> ToCitiesList(List<City> cities)
        {
            List<CityDto> dto = new();

            foreach (var c in cities)
            {
                dto.Add
                (
                    new CityDto
                    {
                        Name = c.Name,
                    }
                );
            }

            return dto;
        }
    }
}
