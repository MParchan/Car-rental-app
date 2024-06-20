using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.LocationServis;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController: ControllerBase
    {
        private readonly ILocationServis _locationService;
        private readonly IMapper _mapper;
        public LocationController(ILocationServis locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        // GET: api/locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationViewModelGet>>> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(_mapper.Map<List<LocationViewModelGet>>(locations));
        }

        // GET: api/locations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationViewModelGet>> GetLocationById(int id)
        {
            try
            {
                var location = await _locationService.GetLocationByIdAsync(id);
                return Ok(_mapper.Map<LocationViewModelGet>(location));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        // POST: api/locations
        [HttpPost]
        public async Task<ActionResult<LocationViewModelGet>> AddLocation(LocationViewModelPost location)
        {
            try
            {
                var createdLocation = await _locationService.AddLocationAsync(_mapper.Map<LocationDto>(location));
                return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.LocationId }, createdLocation);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // PUT: api/locations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationViewModelPut location)
        {
            try
            {
                if (id != location.LocationId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _locationService.UpdateLocationAsync(_mapper.Map<LocationDto>(location));

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

        // DELETE: api/locations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                await _locationService.DeleteLocationAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
