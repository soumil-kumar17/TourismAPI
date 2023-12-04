using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tourism.Repository.Contracts;
using Tourism.Repository.DTO;
using Tourism.Repository.Models;

namespace TourismAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _config;

        public LoginController(IRepositoryWrapper repository, IMapper mapper, ILogger<LoginController> logger, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// Action method to get userprofile for userName and validate password
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userLogin)
        {
            try
            {
                _logger.LogInformation("Calling Authenticate to get user login data from DB.");
                Task<UserProfile> user = _repository.User.GetUser(userLogin.UserName);

                //Check if Branches list has any data
                if (user.Result == null)
                {
                    _logger.LogError($"Authenticate - no user data found");
                    return NotFound($"Authenticate - no user data found for {userLogin.UserName}.");
                }

                UserProfile userResult = _mapper.Map<UserProfile>(user.Result);

                string strPwd = Utility.DecryptPassword(userResult.Password);
                if (strPwd == userLogin.Password)
                {
                    _logger.LogInformation($"User logged in successfully");

                    // generate jwt token
                    string token = GenerateToken(userResult.UserName, userResult.Role);
                    // build user model

                    UserDto userModel = new UserDto
                    {
                        UserName = userResult.UserName,
                        Role = userResult.Role,
                        Token = token
                    };

                    return Ok(userModel);
                }
                else
                {
                    _logger.LogInformation($"LoginUser - password does not match - {userLogin.UserName}");
                    return StatusCode(500, "LoginUser - password does not match.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"LoginUser - Something went wrong inside LoginUser action: {ex}");
                return StatusCode(500, "LoginUser - Internal server error");
            }
        }

        /// <summary>
        /// To generate token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GenerateToken(string userName, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
