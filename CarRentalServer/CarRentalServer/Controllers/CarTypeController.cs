using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/car-types")]
    [ApiController]
    public class CarTypeController : ControllerBase
    {
        private readonly ICarTypeService _carTypeService;
        private readonly IMapper _mapper;
        public CarTypeController(ICarTypeService carTypeService, IMapper mapper)
        {
            _carTypeService = carTypeService;
            _mapper = mapper;
        }

        // GET: api/car-types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarTypeViewModelGet>>> GetAllCarTypes()
        {
            var carTypes = await _carTypeService.GetAllCarTypesAsync();
            return Ok(_mapper.Map<List<CarTypeViewModelGet>>(carTypes));
        }

        // GET: api/car-types/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarTypeViewModelGet>> GetCarTypeById(int id)
        {
            try
            {
                var carType = await _carTypeService.GetCarTypeByIdAsync(id);
                return Ok(_mapper.Map<CarTypeViewModelGet>(carType));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        // POST: api/car-types
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<CarTypeViewModelGet>> AddCarType(CarTypeViewModelPost carType)
        {
            try
            {
                var createdCarType = await _carTypeService.AddCarTypeAsync(_mapper.Map<CarTypeDto>(carType));
                return CreatedAtAction(nameof(GetCarTypeById), new { id = createdCarType.CarTypeId }, createdCarType);
            }
            catch(ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // PUT: api/car-types/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateCarType(int id, CarTypeViewModelPut carType)
        {
            try
            {
                if (id != carType.CarTypeId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _carTypeService.UpdateCarTypeAsync(_mapper.Map<CarTypeDto>(carType));

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

        // DELETE: api/car-types/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteCarType(int id)
        {
            try
            {
                await _carTypeService.DeleteCarTypeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
