using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.Mongo.Model;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SantasLittleHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<MongoUser> _signInManager;

        public LoginController(
            SignInManager<MongoUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            Console.WriteLine($"Login attempt for user: {request.Email}");
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Get claims from the logged-in user
                var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
                var userClaims = user != null ? await _signInManager.UserManager.GetClaimsAsync(user) : new List<Claim>();
                var id = user.Id.ToString();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.Email),
                    new Claim(ClaimTypes.NameIdentifier, id),
                };
                claims.AddRange(userClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKEY")));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(30);

                var token = new JwtSecurityToken(
                    "YourIssuer",
                    "YourAudience",
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );
                Console.WriteLine($"\tUser {request.Email} logged in successfully.");
                Console.WriteLine($"\tToken: {new JwtSecurityTokenHandler().WriteToken(token)}");
                return Ok(new { Message = "Login successful", Success = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            Console.WriteLine($"\tLogin attempt failed for user {request.Email}.");
            return Unauthorized(new { Message = "Invalid login attempt", Success = false });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}