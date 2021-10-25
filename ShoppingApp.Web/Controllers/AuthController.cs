using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Core.DTOs.Request;
using ShoppingApp.Core.DTOs.Response;
using ShoppingApp.Core.Entities;
using ShoppingApp.Web.Filters;
using ShoppingApp.Web.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtsettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IOptions<JwtSettings> options, UserManager<ApplicationUser> userManager, ILogger<AuthController> logger)
        {
            _jwtsettings = options.Value;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("RegisterUser")]
        [ValidateModel]
        public async Task<ActionResult<AuthResponse>> RegisterUserAsync([FromBody] UserReqestDto req)
        {
            var user = await _userManager.FindByEmailAsync(req.Email);

            if(user is not null)
            {
                return new AuthResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "User with this email exist",
                };
            }

            user = new ApplicationUser
            {
                Firstname = req.Firstname,
                Lastname = req.Lastname,
                Address = req.Address,
                Email = req.Email,
                PhoneNumber = req.PhoneNumber,
                UserName = req.Username,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, req.Password);

            if (result.Succeeded)
            {
                var token = GenerateAccessToken(user);

                return new AuthResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "User created successfully",
                    Token = token,
                    Success = true
                };
            }

            _logger.LogError("Error occured while creating user", result.Errors);
            return new AuthResponse
            {
                Success = false,
                Message = "Error occured while creating user",
                StatusCode = StatusCodes.Status500InternalServerError,
                Errors = result.Errors
            };
        }


        private string GenerateAccessToken(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.Email, identityUser.Email)
                }),

                Expires = DateTime.Now.AddMinutes(_jwtsettings.ExpiryTimeInMins),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                //Audience = _jwtsettings.Audience,
                Issuer = _jwtsettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
