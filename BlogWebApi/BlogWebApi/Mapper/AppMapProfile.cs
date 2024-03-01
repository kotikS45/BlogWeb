using AutoMapper;
using BlogWebApi.Data;
using BlogWebApi.Data.Entities;
using BlogWebApi.Models.Category;
using BlogWebApi.Models.Tag;

namespace BlogWebApi.Mapper
{
    public class AppMapProfile : Profile
    {
        private readonly AppEFContext _context;
        public AppMapProfile (AppEFContext appEFContext)
        {
            _context = appEFContext;

            CreateMap<CategoryEntity, CategoryItemViewModel>();
            CreateMap<CategoryCreateViewModel, CategoryEntity>();

            CreateMap<TagEntity, TagItemViewModel>();
            CreateMap<TagCreateViewModel, TagEntity>();
        }
    }
}