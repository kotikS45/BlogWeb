using BlogWebApi.Data.Entities.Identity;

namespace BlogWebApi.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(UserEntity user);
    }
}
