using AutoMapper;
using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.DTOs;
using CarRentalServer.Service.Services.BrandService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController: ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        // GET: api/brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandViewModelGet>>> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(_mapper.Map<List<BrandViewModelGet>>(brands));
        }

        // GET: api/brands/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandViewModelGet>> GetBrandById(int id)
        {
            try
            {
                var brand = await _brandService.GetBrandByIdAsync(id);
                return Ok(_mapper.Map<BrandViewModelGet>(brand));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        // POST: api/brands
        [HttpPost]
        public async Task<ActionResult<BrandViewModelGet>> AddBrand(BrandViewModelPost brand)
        {
            try
            {
                var createdBrand = await _brandService.AddBrandAsync(_mapper.Map<BrandDto>(brand));
                return CreatedAtAction(nameof(GetBrandById), new { id = createdBrand.BrandId }, createdBrand);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        // PUT: api/brands/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, BrandViewModelPut brand)
        {
            try
            {
                if (id != brand.BrandId)
                {
                    return BadRequest(new { ErrorMessage = "Invalid Id" });
                }
                await _brandService.UpdateBrandAsync(_mapper.Map<BrandDto>(brand));

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

        // DELETE: api/brands/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                await _brandService.DeleteBrandAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
