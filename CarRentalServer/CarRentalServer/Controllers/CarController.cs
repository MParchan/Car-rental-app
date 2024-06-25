using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarService;
using CarRentalServer.Service.Services.LocationCarServis;
using CarRentalServer.Service.Services.ReservationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILocationCarService _locationCarService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        public CarController(ICarService carService, ILocationCarService locationCarService, IReservationService reservationService, IMapper mapper)
        {
            _carService = carService;
            _locationCarService = locationCarService;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarViewModelGet>>> GetAllCars()
        {
            try
            {
                var cars = await _carService.GetAllCarsAsync();
                return Ok(_mapper.Map<List<CarViewModelGet>>(cars));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }


        // GET: api/cars/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<CarQuantityViewModel>>> GetAvailableCars(
            [FromQuery, Required] int modelId, [FromQuery, Required] int locationId, [FromQuery, Required] DateTime startDate, [FromQuery, Required] DateTime endDate)
        {
            try
            {
                var carsQuantity = await _locationCarService.GetLocationCarsByLocationIdAndModelId(locationId, modelId);
                var availableCarsQuantity = await _reservationService.GetCarsQuantityWithoutPendingReservationAsync(carsQuantity, startDate, endDate);
                return Ok(availableCarsQuantity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // GET: api/cars/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarViewModelGet>> GetCarById(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                return Ok(_mapper.Map<CarViewModelGet>(car));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // POST: api/cars
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<CarViewModelPost>> AddCar(CarViewModelPost car)
        {
            try
            {
                var createdCar = await _carService.AddCarAsync(_mapper.Map<CarDto>(car));
                return CreatedAtAction(nameof(GetCarById), new { id = createdCar.CarId }, createdCar);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // PUT: api/cars/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateCar(int id, CarViewModelPut car)
        {
            try
            {
                if (id != car.CarId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _carService.UpdateCarAsync(_mapper.Map<CarDto>(car));

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
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // DELETE: api/cars/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carService.DeleteCarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }  
    }
}
