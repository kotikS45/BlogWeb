using AutoMapper;
using BlogWebApi.Data;
using BlogWebApi.Data.Entities;
using BlogWebApi.Data.Entities.Identity;
using BlogWebApi.Models.Account;
using BlogWebApi.Models.Category;
using BlogWebApi.Models.Post;
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

            CreateMap<PostEntity, PostItemViewModel>()
                .ForMember(x => x.Tags, opt => opt.Ignore());
            CreateMap<PostCreateViewModel, PostEntity>()
                .ForMember(x => x.Tags, opt => opt.Ignore());
            CreateMap<PostEditViewModel, PostEntity>()
                .ForMember(x => x.Tags, opt => opt.Ignore());

            CreateMap<UserEntity, AccountItemViewModel>();
        }
    }
}