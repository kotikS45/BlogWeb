using AutoMapper;
using BlogWebApi.Constants;
using BlogWebApi.Data.Entities.Identity;
using BlogWebApi.Helpers;
using BlogWebApi.Interfaces;
using BlogWebApi.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                    if (user == null)
                        return BadRequest("User not found");
                }
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    return BadRequest("Incorrect password");

                var token = await _jwtTokenService.CreateTokenAsync(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] RegistrationViewModel model)
        {
            try
            {
                var user = new UserEntity
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, Roles.User);
                }
                else
                {
                    return BadRequest("Error code 500");
                }
                var token = await _jwtTokenService.CreateTokenAsync(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("avatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar(AccountAvatarUpdateModel model)
        {
            try
            {
                string email = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(email);

                if (model.Image != null)
                {
                    string fileRemove = Path.Combine(Directory.GetCurrentDirectory(), "images", user.Image);
                    if (System.IO.File.Exists(fileRemove))
                        System.IO.File.Delete(fileRemove);
                    user.Image = await ImageWorker.SaveImageAsync(model.Image);
                }
                else 
                    user.Image = null;

                await _userManager.UpdateAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            try
            {
                string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return BadRequest("User not found");
                var userMapped = _mapper.Map<AccountItemViewModel>(user);

                return Ok(userMapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
