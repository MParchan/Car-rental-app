using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.CarService;
using CarRentalServer.Service.Services.ModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelController: ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;
        public ModelController(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        // GET: api/models
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelViewModelGet>>> GetAllModels()
        {
            try
            {
                var models = await _modelService.GetAllModelsAsync();
                return Ok(_mapper.Map<List<ModelViewModelGet>>(models));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // GET: api/models/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelViewModelGet>> GetModelById(int id)
        {
            try
            {
                var model = await _modelService.GetModelByIdAsync(id);
                return Ok(_mapper.Map<ModelViewModelGet>(model));
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

        // POST: api/models
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<ModelViewModelPost>> AddModel(ModelViewModelPost model)
        {
            try
            {
                var createdModel = await _modelService.AddModelAsync(_mapper.Map<ModelDto>(model));
                return CreatedAtAction(nameof(GetModelById), new { id = createdModel.ModelId }, createdModel);
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

        // PUT: api/models/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateModel(int id, ModelViewModelPut model)
        {
            try
            {
                if (id != model.ModelId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _modelService.UpdateModelAsync(_mapper.Map<ModelDto>(model));

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

        // DELETE: api/models/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            try
            {
                await _modelService.DeleteModelAsync(id);
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
