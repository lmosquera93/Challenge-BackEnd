using Challenge_WirTrack.DataAccess;
using Challenge_WirTrack.Entities;
using Challenge_WirTrack.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Challenge_WirTrack.Controllers
{
    [Route("travels/")]
    [ApiController]

    public class TravelController : ControllerBase
    {
        private readonly WirtrackDbContext _context;

        public TravelController(WirtrackDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertDto dto)
        {

            try
            {
                var newTravel = new Travel()
                {
                    Date = dto.Date,
                    CityID = dto.CityID,
                    VehicleID = dto.VehicleID
                };
                //Busca si ese vehiculo tiene un vieaje programado ese dia. Solo se puede hacer un viaje por dia.
                
                var search = await _context.Travels.FirstOrDefaultAsync(x => x.Date == dto.Date && x.VehicleID == dto.VehicleID);

                
                if (search == null)
                {
                    var searchCity = await _context.Cities.FindAsync(dto.CityID);

                    if(searchCity.Name == "Buenos Aires" || searchCity.Name == "London" || searchCity.Name == "La Plata") {

                        bool boolean = getWeather(searchCity.Name, dto.Date);

                        if (boolean == true)
                        {
                            return BadRequest(new { message = "Esta lloviendo." });
                        }
                        _context.Travels.Add(newTravel);

                        await _context.SaveChangesAsync();

                        return Ok(new { message = "Travel Created!" });
                    }
                    else
                    {
                        return BadRequest(new { message = "Solo se puede viajar a London, Buenos Aires o La Plata." });
                    }

                }

                return BadRequest(new { message = "Ya tiene un viaje Programado este dia." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listTravels = await _context.Travels.Include(x => x.City).Include(x => x.Vehicle).Where(x => x.IsDeleted == false).ToListAsync();

                List<TravelDto> traveldto = new();

                foreach (var c in listTravels)
                {
                    traveldto.Add
                    (
                        new TravelDto
                        {
                            Id = c.Id,
                            Date = c.Date,
                            CityID = c.CityID,
                            VehicleID = c.VehicleID,
                            City = new City
                            {
                                Name = c.City.Name,
                            },
                            Vehicle = new Vehicle
                            {
                                Patent = c.Vehicle.Patent,
                                Type = c.Vehicle.Type,
                                Brand = c.Vehicle.Brand
                            }
                        }
                    );
                }

                return Ok(listTravels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var travel = await _context.Travels.FindAsync(Id);

                if (travel == null)
                {
                    return NotFound();
                }
                else
                {
                    travel.IsDeleted = true;

                    await _context.SaveChangesAsync();

                    return Ok(new {message = "Deleted!"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update(TravelUpdateDto dto, int Id)
        {
            try
            {
                var findTravel = await _context.Travels.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefaultAsync();

                if (findTravel == null)
                {
                    return NotFound();
                }
                else
                {
                    //When modify City automatically update LastModify 
                    findTravel.LastModified = DateTime.Now;
                    findTravel.Date = dto.Date;
                    findTravel.VehicleID = dto.VehicleID;
                    findTravel.CityID = dto.CityID;
                    
                    _context.Travels.Update(findTravel);

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Update Correct!" });
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public bool getWeather(string City , DateTime Date)
        {
            using (WebClient client = new WebClient())
            {
                
                string ApiID = "f81077568dac3512b6c107cc75b05b6d";
                string Uri = "http://api.openweathermap.org/";
                string URLCity = Uri+$"data/2.5/forecast?q={City}&appid={ApiID}";
                string URL = "";

                switch (City)
                {
                    case "Buenos Aires":
                        URL = Uri + $"data/2.5/onecall?lat=-34.6132&lon=-58.3772&exclude=current,hourly,minutely,alerts&units=metric&appid={ApiID}";
                        break;
                    case "La Plata":
                        URL = Uri + $"data/2.5/onecall?lat=-34.9215&lon=-57.9545&exclude=current,hourly,minutely,alerts&units=metric&appid={ApiID}";
                        break;
                    case "London":
                        URL = Uri + $"data/2.5/onecall?lat=-51.5085&lon=-0.1257&exclude=current,hourly,minutely,alerts&units=metric&appid={ApiID}";
                        break;

                }


                var json = client.DownloadString(URL);

                var data = JsonConvert.DeserializeObject<WeatherInfoDto.root>(json);

                //var list = data.daily;

                int day = Date.Day;

                int daycompare = DateTime.Today.Day;

                var list = data.daily[0];

                switch (day - daycompare)
                {
                    //Today
                    case 0:
                        list = data.daily[0];
                        break;
                      
                    //Today+1
                    case 1:
                        list = data.daily[1];
                        break;
                    //Today+2
                    case 2:
                        list = data.daily[2];
                        break;
                    //Today+3
                    case 3:
                        list = data.daily[3];
                        break;
                    //Today+4
                    case 4:
                        list = data.daily[4];
                        break;
                    //Today+5
                    case 5:
                        list = data.daily[5];
                        break; 
                    //Today+6
                    case 6:
                        list = data.daily[6];
                        break;
                }

                if (list.weather[0].main == "Rain")
                {
                    return true;
                }
                return false;

                      

            }
        }


    }
}
