using BlogWebApi.Constants;
using BlogWebApi.Data.Entities.Identity;
using BlogWebApi.Helpers;
using BlogWebApi.Interfaces;
using BlogWebApi.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return BadRequest("User not found");
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
                string image = string.Empty;
                if (!string.IsNullOrEmpty(model.base64))
                {
                    image = await ImageWorker.SaveImageAsync(model.base64);
                }

                string email = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(email);

                if (model.base64.Length > 0)
                    user.Image = image;
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
    }
}
