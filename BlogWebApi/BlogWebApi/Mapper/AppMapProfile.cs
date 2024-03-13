using AutoMapper;
using BlogWebApi.Data;
using BlogWebApi.Data.Entities;
using BlogWebApi.Data.Entities.Identity;
using BlogWebApi.Models.Account;
using BlogWebApi.Models.Category;
using BlogWebApi.Models.Post;
using BlogWebApi.Models.Tag;
using Microsoft.EntityFrameworkCore;

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

            CreateMap<UserEntity, AccountItemViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => GetRolesForUser(src, _context)));
        }

        private ICollection<string  > GetRolesForUser(UserEntity user, AppEFContext context)
        {
            var userRoles = context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .ToList();

            var roles = new List<string>();

            for (var i = 0; i < userRoles.Count; i++)
            {
                roles.Add(context.Roles.FirstOrDefault(x => x.Id == userRoles[i].RoleId).Name);
            }

            return roles;
        }
    }
}