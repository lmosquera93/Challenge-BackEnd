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

                // esta lloviendo hoy? 
                if (search == null)
                {
                    _context.Travels.Add(newTravel);

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Travel Created!" });
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
                //string strin = "La Plata";
                //string dateTime = "2022 - 07 - 14 06:00:00";
                //getWeather(strin, dateTime);

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
                var findTravel = await _context.Travels.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (findTravel == null)
                {
                    return NotFound();
                }
                else
                {
                    //When modify City automatically update LastModify and if it is deleted then is back on "False".
                    findTravel.LastModified = DateTime.Now;
                    findTravel.Date = dto.Date;
                    findTravel.VehicleID = dto.VehicleID;
                    findTravel.CityID = dto.CityID;
                    findTravel.IsDeleted = false;

                    _context.Travels.Update(findTravel);

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Update Correct!" });
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Only 4 days brings
        public bool getWeather(string City, string Date)
        {
            using (WebClient client = new WebClient())
            {
                string ApiID = "f81077568dac3512b6c107cc75b05b6d";

                string Uri = "http://api.openweathermap.org/";

                string completeURL = Uri+$"data/2.5/forecast?q={City}&appid={ApiID}";


                string json = client.DownloadString(completeURL);

                WeatherInfoDto.list Info = JsonConvert.DeserializeObject<WeatherInfoDto.list>(json);

                foreach(WeatherInfoDto.Weather weatherInfo in Info.weather)
                {
                    if (Date.Equals(Info.dt_txt) && weatherInfo.main == "Clouds")
                    {
                        return true;
                    }

                }
                return false;

            }
        }

    }
}
