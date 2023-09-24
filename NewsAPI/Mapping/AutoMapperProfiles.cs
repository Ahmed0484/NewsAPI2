using AutoMapper;
using NewsAPI.Models.DTOs;
using NewsAPI.Models;

namespace NewsAPI.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<CreateNewsDto, News>();
            CreateMap<UpdateNewsDto, News>();
        }
    }
}
