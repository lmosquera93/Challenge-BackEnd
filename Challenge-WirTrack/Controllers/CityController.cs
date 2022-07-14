using Challenge_WirTrack.DataAccess;
using Challenge_WirTrack.Entities;
using Challenge_WirTrack.Models.Dtos;
using Challenge_WirTrack.Models.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_WirTrack.Controllers
{
    [Route("cities/")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly WirtrackDbContext _context;

        public CityController(WirtrackDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CityDto dto)
        {
            try
            {

                var newCity = new City()
                {
                    Name = dto.Name
                };

                _context.Cities.Add(newCity);

                await _context.SaveChangesAsync();

                return Ok(new { message = "City Created!" });

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
                var listCities = await _context.Cities.Where(x => x.IsDeleted == false).ToListAsync();

                return Ok(listCities);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete (int Id)
        {

            try
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == Id);

                if (city == null)
                {
                    return NotFound();
                }
                else
                {
                    city.IsDeleted = true;

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Deleted!" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update(CityDto dto, int Id)
        {
            try
            {
                var findcity = await _context.Cities.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (findcity == null)
                {
                    return NotFound();
                }
                else
                {
                    //When modify City automatically update LastModify and if it is deleted then is back on "False".
                    findcity.LastModified = DateTime.Now;
                    findcity.Name = dto.Name;
                    findcity.IsDeleted = false;

                    _context.Cities.Update(findcity);

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Update Correct!" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
