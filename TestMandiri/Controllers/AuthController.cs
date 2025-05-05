using Azure.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TestMandiri.Data.Models;
using TestMandiri.Services;
using YourNamespace.Services.Interfaces;

namespace TestMandiri.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromQuery] string username, string password)
        {
            var result = _authService.Login(username, password);
            if (result == "Login berhasil.")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            try
            {
                var detaildata = new MsdetailUser
                {
                    Umur=model.umur,
                    Nama= model.nama,
                    Tanggallahir= model.tanggal
                };
                var result = await _authService.Register(model.username, model.password, detaildata);
                if (result == "ok")
                    return Ok(new { message = "simpan data success" });
                return BadRequest(new { message = result });
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(nameof(Register), ex);
                return BadRequest(new { message = "mohon hubungi tim support" });
            }
        }
    }
}
