using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Mapping;
using NewsAPI.Models;
using NewsAPI.Models.DTOs;
using NewsAPI.Repositories;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles="Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CategoryDto>>(await _repo.GetAllAsync()));
        }
        [Authorize(Roles="Reader,Writer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        [Authorize(Roles="Writer")]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            await _repo.CreateAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetById), new { Id = category.Id }, categoryDto);
        }

        [Authorize(Roles="Writer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            category = await _repo.UpdateAsync(id, category);
            if(category == null) return NotFound();
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [Authorize(Roles="Writer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repo.DeleteAsync(id);
            if (category == null) return NotFound();
            return Ok(_mapper.Map<CategoryDto>(category));
        }
    }
}
