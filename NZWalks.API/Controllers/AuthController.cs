using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // Post /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequest.Usermame,
                Email = registerRequest.Usermame
            };
            var identityResult = await userManager.CreateAsync(IdentityUser, registerRequest.Password);
            if (identityResult.Succeeded) {
                // Add Roles
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    await userManager.AddToRolesAsync(IdentityUser, registerRequest.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was register Successfully");
                    }


                }


            }
            return BadRequest("Somethin Went Wrong");
        }

        // Post /api/Auth/Register
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
         var user =  await userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null) {
              var  checkPasswordRes = await userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (checkPasswordRes)
                {
                    // Create JWT-Token
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null) {
                     var jwtToken = tokenRepository.CreateJWTToken(user,roles.ToList());
                        var loginResponse = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                            Usermame = loginRequest.Username,
                            Roles = roles.ToList().ToArray(),

                        };
                        return Ok(loginResponse);

                    }
                    
                   
                }
            }
            return BadRequest("UserName or password was wrong");
        }
    }

}
    


