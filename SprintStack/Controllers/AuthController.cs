using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SprintStack.DTOs;

using SprintStack.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SprintStack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IdentityServiceChecker _identityChecker;
        private readonly IConfiguration _config;

        public AuthController(UserService userService, IdentityServiceChecker identityChecker, IConfiguration config)
        {
            _userService = userService;
            _identityChecker = identityChecker;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var isIdentityAvailable = await _identityChecker.IsIdentityServiceAvailable();
            if (!isIdentityAvailable)
            {
                // Fallback: Local DB validation
                var user = await _userService.ValidateUserAsync(request.Email, request.Password);
                if (user == null)
                    return Unauthorized("Invalid credentials.");

                var token = GenerateJwtToken(user);
                return Ok(new
                {
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        RoleId = user.RoleId
                    }
                });
            }

            return StatusCode(503, "Identity service is UP. Use it instead for login.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var isIdentityAvailable = await _identityChecker.IsIdentityServiceAvailable();
            if (!isIdentityAvailable)
            {
                var user = await _userService.RegisterUserAsync(request);
                var token = GenerateJwtToken(user);
                return Ok(new
                {
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        RoleId = user.RoleId
                    }
                });
            }

            return StatusCode(503, "Identity service is UP. Use it instead for registration.");
        }

        private string GenerateJwtToken(User user)
        {
            
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("identity-status")]
        public async Task<IActionResult> GetIdentityServiceStatus()
        {
            bool isAvailable = await _identityChecker.IsIdentityServiceAvailable();
            return Ok(new { status = isAvailable ? "UP" : "DOWN" });
        }
    }
}
