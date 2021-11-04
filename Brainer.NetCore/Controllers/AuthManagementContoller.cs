using Brainer.NetCore.Configuration;
using Brainer.NetCore.Models.DTOs.Requests;
using Brainer.NetCore.Models.DTOs.Responses;
using Brainer.NetCore.Models.Entities;
using Brainer.NetCore.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Brainer.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementContoller : ControllerBase
    {
        private readonly ApplicationUserManager _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementContoller(ApplicationUserManager userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            this._userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(userLoginRequest.Email);
                if(existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid login request"
                        },
                        Succes = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userLoginRequest.Password);
                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid payload"
                        },
                        Succes = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);
                return Ok(new RegistrationResponse()
                {
                    Succes = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                        {
                            "Invalid payload"
                        },
                Succes = false
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistrationDTOs user)
        {
            Console.WriteLine("incoming request");
            if (ModelState.IsValid)
            {
                if (!user.Password.Equals(user.PasswordCheck))
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Passwords must match"
                        },
                        Succes = false
                    });
                }
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if(existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Email already in use"
                        },
                        Succes = false
                    });
                }
                var newUser = new ApplicationUser()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.Email
                };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new RegistrationResponse()
                    {
                        Succes = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    var message = string.Join(", ", isCreated.Errors.Select(x => "Code " + x.Code + " Description" + x.Description));
                    return BadRequest($"unable to create user one field is wrong : {message}");
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                        {
                            "Invalid payload"
                        },
                Succes = false
            });
        }

        private string GenerateJwtToken(IdentityUser identityUser)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", identityUser.Id),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }

}
