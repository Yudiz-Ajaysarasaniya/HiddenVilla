using Common;
using DataAccess.Data.Entities;
using HiddenVilla_Api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Request;
using Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HiddenVilla_Api.Controllers
{
    //[Route("api/[controller]/[action]")] // added but i have comment out
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly APISettings apiSettings;

        public AccountController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IOptions<APISettings> options)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            apiSettings = options.Value;
        }

        #region SignUp
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserRegistrationRequest request)
        {
            if(request == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.Phone,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.Select(x => x.Description);
                return BadRequest(new UserRegisterResponse
                {
                    IsSuccess = false,
                    Errors = error.ToList()
                });
            }

            var role = await userManager.AddToRoleAsync(user, Roles.Role_Customer);

            if (!role.Succeeded)
            {
                var error = result.Errors.Select(x => x.Description);
                return BadRequest(new UserRegisterResponse
                {
                    IsSuccess = false,
                    Errors = (List<string>)error
                });
            }
            //return Ok(StatusCodes.Status200OK);
            return StatusCode(201);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(request.Email);
                if (user == null)
                {
                    return Unauthorized(new UserLoginResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                var signinCredentials = GetSigningCredentials();
                var claims = await GetUserClaim(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: apiSettings.ValidIssuer, 
                    audience: apiSettings.ValidAudience,
                    claims: claims,
                    expires:DateTime.Now.AddDays(3),
                    signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new UserLoginResponse
                {
                    IsSuccess = true,
                    Token = token,
                    user = new User
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        Phone = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new UserLoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid authentication"
                });
            }
        }
        #endregion

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.Key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetUserClaim(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };

            var roles = await userManager.GetRolesAsync(await userManager.FindByEmailAsync(user.Email));

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
