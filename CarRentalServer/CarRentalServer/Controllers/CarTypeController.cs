using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarTypeService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/CarType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarTypeViewModel>>> GetAllCarTypes()
        {
            var carTypes = await _carTypeService.GetAllCarTypesAsync();
            return Ok(_mapper.Map<List<CarTypeViewModel>>(carTypes));
        }

        // GET: api/CarType/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<CarTypeViewModel>> GetCarTypeById(int id)
        {
            try
            {
                var carType = await _carTypeService.GetCarTypeByIdAsync(id);
                return Ok(_mapper.Map<CarTypeViewModel>(carType));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Car type not found");
            }
        }

        // POST: api/CarType
        [HttpPost]
        public async Task<ActionResult<CarTypeViewModel>> AddCarType(CarTypeViewModel carType)
        {
            try
            {
                await _carTypeService.AddCarTypeAsync(_mapper.Map<CarTypeDto>(carType));
                return CreatedAtAction(nameof(GetCarTypeById), new { id = carType.CarTypeId }, carType);
            }
            catch(ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // PUT: api/CarType/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarType(int id, CarTypeViewModel carType)
        {
            try
            {
                if (id != carType.CarTypeId)
                {
                    return BadRequest("Invalid Id");
                }
                await _carTypeService.UpdateCarTypeAsync(_mapper.Map<CarTypeDto>(carType));

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Car type not found");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // DELETE: api/CarType/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarType(int id)
        {
            try
            {
                await _carTypeService.DeleteCarTypeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Car type not found");
            }
        }
    }
}
