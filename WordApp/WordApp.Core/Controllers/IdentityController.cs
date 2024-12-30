using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WordApp.Data;
using WordApp.Dtos;

namespace WordApp.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Yeni bir kullanıcı oluşturuyoruz.
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FullName = registerDto.FullName // ApplicationUser modelinde FullName varsa kullanıyoruz.
            };

            // Şifreyi hash'leyerek kullanıcıyı kaydediyoruz.
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                // Kayıt başarısızsa, hataları dönüyoruz.
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User registered successfully!" });
        }
    }
}
