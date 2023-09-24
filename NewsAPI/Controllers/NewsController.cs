using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Models.DTOs;
using NewsAPI.Repositories;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepo _repo;
        private readonly IMapper _mapper;

        public NewsController(INewsRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsDto createNewsDto)
        {
            if (createNewsDto == null) { return BadRequest(); }
            var news = _mapper.Map<News>(createNewsDto);
            await _repo.CreateAsync(news);
            var newsDto = _mapper.Map<NewsDto>(news);
            //return CreatedAtAction();
            return Ok(newsDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string filterOn,string filterQuery,
            string sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 1000)
        {
          
            var news=await _repo.GetAllAsync(filterOn,filterQuery,sortBy,isAscending??true,pageNumber,pageSize);
            var newsDto = _mapper.Map<List<NewsDto>>(news);
            return Ok(newsDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var news = await _repo.GetByIdAsync(id);
            if(news == null) { return NotFound(); } 
            var newsDto = _mapper.Map<NewsDto>(news);
            return Ok(newsDto);
        }
        [HttpPut("{id:int}")]
     
        public async Task<IActionResult> Update(int id,UpdateNewsDto updateNewsDto)
        {
            
                var news = _mapper.Map<News>(updateNewsDto);
                news = await _repo.UpdateAsync(id, news);
                if (news == null) { return NotFound(); }
                var newsDto = _mapper.Map<NewsDto>(news);
                return Ok(newsDto);
           
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _repo.DeleteAsync(id);
            if (news == null) { return NotFound(); }
            var newsDto = _mapper.Map<NewsDto>(news);
            return Ok(newsDto);
        }
    }
}
