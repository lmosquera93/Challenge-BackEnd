using Challenge_WirTrack.DataAccess;
using Challenge_WirTrack.Entities;
using Challenge_WirTrack.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_WirTrack.Controllers
{
    [Route("vehicles/")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly WirtrackDbContext _context;
        public VehicleController(WirtrackDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Insert(VehicleDto dto)
        {
            try
            {
                var newVehicle = new Vehicle()
                {
                    Type = dto.Type,
                    Brand = dto.Brand,
                    Patent = dto.Patent
                };

                _context.Vehicles.Add(newVehicle);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Vehicle Created!" });
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

                List<Vehicle> listVehicles = await _context.Vehicles.Where(x => x.IsDeleted == false).ToListAsync();

                return Ok(listVehicles);

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
                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == Id);

                if (vehicle == null)
                {
                    return NotFound(new { message = "Not Found!" });
                }
                else
                {
                    vehicle.IsDeleted = true;

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
        public async Task<IActionResult> Update(VehicleDto dto, int Id)
        {

            try
            {
                var findVehicle = await _context.Vehicles.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefaultAsync();

                if (findVehicle == null)
                {
                    return NotFound(new { message = "Not Found!" });
                }
                else
                {
                    
                    findVehicle.LastModified = DateTime.Now;
                    findVehicle.Type = dto.Type;
                    findVehicle.Brand = dto.Brand;
                    findVehicle.Patent = dto.Patent;

                    _context.Vehicles.Update(findVehicle);

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
