using HealthRecordApp.Authentication.Configuration;
using HealthRecordApp.DataService.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using HealthRecordApp.Authentication.DTO.Incoming;
using HealthRecordApp.Authentication.DTO.Outgoing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HealthRecordApp.API.Controllers.V1
{

    public class AccountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _JWTConfig;
        public AccountController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager,
            IOptionsMonitor<JWTConfig> optionsMonitor) : base(unitOfWork) 
        {
            _JWTConfig = optionsMonitor.CurrentValue;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register ([FromBody]UserRegisterationRequestDto userRegisterationDto)
        {
            if(ModelState.IsValid)
            {
                //check if email is exisitng
                var user = await _userManager.FindByEmailAsync(userRegisterationDto.email);
                if (user != null)
                {
                    return BadRequest(new UserRegisterationResponseDto()
                    {
                        Token = "",
                        Success = false,
                        Errors = new List<string>() { "Email already in use" }
                    });
                }

                //add the user 
                var newUser = new IdentityUser()
                {
                    Email = userRegisterationDto.email,
                    UserName=userRegisterationDto.email,
                    EmailConfirmed=true,//todi send email to user to confirm his email

                };

                var isCreated= await _userManager.CreateAsync(newUser, userRegisterationDto.password);
                if(!isCreated.Succeeded)
                {
                    return BadRequest(new UserRegisterationResponseDto()
                    {
                        Token = "",
                        Success = false,
                        Errors = isCreated.Errors.Select(e=>e.Description).ToList()
                    });
                }

                //create JWT Token
                var token= GenerateToken(newUser);

                return Ok(new UserRegisterationResponseDto()
                {
                    Token=token,
                    Success = true,

                });
            }
            else
            {
                return BadRequest(new UserRegisterationResponseDto()
                {
                    Token = "",
                    Success = false,
                    Errors=new List<string>() { "Invalid Payload"}
                });
            }
        }

        private string GenerateToken(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_JWTConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email), //unique id
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())//unique identifier for the generated token not the user ,used for referesh token   
            }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time     
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //the algorithm for token encryption 
            };
            //create security object token
            var token = jwtHandler.CreateToken(tokenDescriptor);
            //convert security object token into string 
            return jwtHandler.WriteToken(token);
        }
    }
}
