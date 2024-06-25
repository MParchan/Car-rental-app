using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleSrvice;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleSrvice, IMapper mapper)
        {
            _roleSrvice = roleSrvice;
            _mapper = mapper;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewModelGet>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleSrvice.GetAllRolesAsync();
                return Ok(_mapper.Map<List<RoleViewModelGet>>(roles));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // GET: api/roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleViewModelGet>> GetRoleById(int id)
        {
            try
            {
                var role = await _roleSrvice.GetRoleByIdAsync(id);
                return Ok(_mapper.Map<RoleViewModelGet>(role));
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

        // POST: api/roles
        [HttpPost]
        public async Task<ActionResult<RoleViewModelGet>> AddRole(RoleViewModelPost role)
        {
            try
            {
                var createdRole = await _roleSrvice.AddRoleAsync(_mapper.Map<RoleDto>(role));
                return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.RoleId }, createdRole);
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

        // PUT: api/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleViewModelPut role)
        {
            try
            {
                if (id != role.RoleId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _roleSrvice.UpdateRoleAsync(_mapper.Map<RoleDto>(role));

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

        // DELETE: api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                await _roleSrvice.DeleteRoleAsync(id);
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
