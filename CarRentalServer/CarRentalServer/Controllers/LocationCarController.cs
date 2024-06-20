using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.LocationCarServis;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/location-cars")]
    [ApiController]
    public class LocationCarController: ControllerBase
    {
        private readonly ILocationCarServis _locationCarService;
        private readonly IMapper _mapper;
        public LocationCarController(ILocationCarServis locationCarService, IMapper mapper)
        {
            _locationCarService = locationCarService;
            _mapper = mapper;
        }

        // GET: api/location-cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationCarViewModelGet>>> GetAllLocationCars()
        {
            var locationCars = await _locationCarService.GetAllLocationCarsAsync();
            return Ok(_mapper.Map<List<LocationCarViewModelGet>>(locationCars));
        }

        // GET: api/location-cars/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationCarViewModelGet>> GetLocationCarById(int id)
        {
            try
            {
                var locationCar = await _locationCarService.GetLocationCarByIdAsync(id);
                return Ok(_mapper.Map<LocationCarViewModelGet>(locationCar));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        // POST: api/location-cars
        [HttpPost]
        public async Task<ActionResult<LocationCarViewModelGet>> AddLocationCar(LocationCarViewModelPost locationCar)
        {
            try
            {
                var createdLocationCar = await _locationCarService.AddLocationCarAsync(_mapper.Map<LocationCarDto>(locationCar));
                return CreatedAtAction(nameof(GetLocationCarById), new { id = createdLocationCar.LocationCarId }, createdLocationCar);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // PUT: api/location-cars/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocationCar(int id, LocationCarViewModelPut locationCar)
        {
            try
            {
                if (id != locationCar.LocationCarId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _locationCarService.UpdateLocationCarAsync(_mapper.Map<LocationCarDto>(locationCar));

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // DELETE: api/location-cars/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationCar(int id)
        {
            try
            {
                await _locationCarService.DeleteLocationCarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
