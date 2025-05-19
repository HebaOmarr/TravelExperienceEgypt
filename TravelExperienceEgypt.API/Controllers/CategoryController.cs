using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExperienceEgypt.BusinessLogic.Services.Contract;
using TravelExperienceEgypt.DataAccess.DTO.Category;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryDTO category = await _categoryService.GetByIDAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategoryName(int id, UpdateCategoryDto categoryDto)
        {
            CategoryDTO category = await _categoryService.GetByIDAsync(id);
            if (category == null)
                return NotFound();

            category.Name = categoryDto.Name;

            await _categoryService.UpdateAsync(category);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }
    }
}
