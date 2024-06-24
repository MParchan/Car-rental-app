using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.LocationCarServis;
using CarRentalServer.Service.Services.ReservationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        // GET: api/reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationViewModelGet>>> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(_mapper.Map<List<ReservationViewModelGet>>(reservations));
        }

        // GET: api/reservations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationViewModelGet>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                return Ok(_mapper.Map<ReservationViewModelGet>(reservation));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<ActionResult<ReservationViewModelGet>> AddReservation(ReservationViewModelPost reservation)
        {
            try
            {
                var createdReservation = await _reservationService.AddReservationAsync(_mapper.Map<ReservationDto>(reservation));
                return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.ReservationId }, createdReservation);
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

        // PUT: api/reservations/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationViewModelPut reservation)
        {
            try
            {
                if (id != reservation.ReservationId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _reservationService.UpdateReservationAsync(_mapper.Map<ReservationDto>(reservation));

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

        // DELETE: api/reservations/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        // PATCH: api/reservations/{id}/start-resrvation
        [HttpPatch("{id}/start-resrvation")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> StartReservation(int id)
        {
            try
            {
                await _reservationService.StartReservationAsync(id);
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

        // PATCH: api/reservations/{id}/end-resrvation
        [HttpPatch("{id}/end-resrvation")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> EndReservation(int id)
        {
            try
            {
                await _reservationService.EndReservationAsync(id);
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

        // PATCH: api/reservations/{id}/cancel-resrvation
        [HttpPatch("{id}/cancel-resrvation")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            try
            {
                await _reservationService.StartReservationAsync(id);
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
    }
}
